using privado_backend.Models.DTOs;
using privado_backend.Models;
using System;
using privado_backend.Data;
using Microsoft.EntityFrameworkCore;

namespace privado_backend.Services
{
    public interface IMenuService
    {
        Task<List<MenuNodeDto>> GetMenuForTipoAsync(int idUsuarioTipo);
    }

    public class MenuService : IMenuService
    {
        private readonly privado_backendContext _db;
        public MenuService(privado_backendContext db) => _db = db;

        public async Task<List<MenuNodeDto>> GetMenuForTipoAsync(int idUsuarioTipo)
        {
            // Helper: normaliza 0 como null
            int? ParentOf(menu m) => (m.id_padre == 0 ? (int?)null : m.id_padre);

            // 1) Menús activos (sin tracking)
            var menus = await _db.menu
                .AsNoTracking()
                .Where(m => m.is_active)
                .OrderBy(m => m.sort_order)
                .ThenBy(m => m.id)
                .ToListAsync();

            if (menus.Count == 0) return new();

            // Índice por id
            var byId = menus.ToDictionary(m => m.id);

            // Mapa de hijos SOLO para padres no nulos (clave no puede ser null)
            var childrenByParent = menus
                .Where(m => ParentOf(m) != null)
                .GroupBy(m => ParentOf(m)!.Value)
                .ToDictionary(g => g.Key, g => g.ToList());

            // 2) Permisos
            var perms = await _db.usuario_tipo_menu
                .AsNoTracking()
                .Where(p => p.id_usuario_tipo == idUsuarioTipo
                            && (p.can_view ?? false)
                            && ((p.estado ?? 1) == 1))
                .ToListAsync();

            var allowed = new HashSet<int>();

            // Descendientes
            void AddDescendants(int id)
            {
                var stack = new Stack<int>();
                stack.Push(id);
                while (stack.Count > 0)
                {
                    var cur = stack.Pop();
                    if (!allowed.Add(cur)) continue;
                    if (childrenByParent.TryGetValue(cur, out var childs))
                        foreach (var c in childs) stack.Push(c.id);
                }
            }

            // Ancestros
            void AddAncestors(int id)
            {
                var cur = id;
                while (byId.TryGetValue(cur, out var node))
                {
                    if (!allowed.Add(node.id)) break;
                    var parent = ParentOf(node);
                    if (parent is null) break; // raíz
                    cur = parent.Value;
                }
            }

            // Aplica cada permiso
            foreach (var p in perms)
            {
                if (!byId.ContainsKey(p.id_menu)) continue;

                if (p.include_descendants ?? false) AddDescendants(p.id_menu);
                else allowed.Add(p.id_menu);

                if (p.include_ancestors ?? false) AddAncestors(p.id_menu);
            }

            if (allowed.Count == 0) return new();

            // Asegurar breadcrumbs para todo permitido
            foreach (var id in allowed.ToList()) AddAncestors(id);

            // 3) Filtrado final
            var filtered = menus.Where(m => allowed.Contains(m.id)).ToList();

            // Rehacer children SOLO con los filtrados
            var filteredChildren = filtered
                .Where(m => ParentOf(m) != null)
                .GroupBy(m => ParentOf(m)!.Value)
                .ToDictionary(g => g.Key, g => g.OrderBy(x => x.sort_order).ThenBy(x => x.id).ToList());

            MenuNodeDto Map(menu m)
            {
                var dto = new MenuNodeDto
                {
                    id = m.id,
                    label = m.label,
                    path = m.path,
                    sort_order = m.sort_order
                };
                if (filteredChildren.TryGetValue(m.id, out var childs))
                    dto.children = childs.Select(Map).ToList();
                return dto;
            }

            // 4) Raíces = id_padre NULL o 0
            var rootsRaw = filtered.Where(m => ParentOf(m) == null)
                                   .OrderBy(m => m.sort_order).ThenBy(m => m.id);

            var roots = rootsRaw.Select(Map).ToList();
            return roots;
        }

    }

}
