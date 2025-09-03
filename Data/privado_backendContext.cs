using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using privado_backend.Models;
using privado_backend.Models.SistemasPrivado;

namespace privado_backend.Data
{
    public class privado_backendContext : DbContext
    {
        public privado_backendContext (DbContextOptions<privado_backendContext> options)
            : base(options)
        {
        }

        public DbSet<cuenta> cuenta { get; set; } = default!;
        public DbSet<cuentaterceros> cuentaterceros { get; set; } = default!;
        public DbSet<cuentatipo> cuentatipo { get; set; } = default!;
        public DbSet<departamento> departamento { get; set; } = default!;
        public DbSet<monedatipo> monedatipo { get; set; } = default!;
        public DbSet<municipio> municipio { get; set; } = default!;
        public DbSet<operaciones> operaciones { get; set; } = default!;
        public DbSet<producto_bancario_tipo> producto_bancario_tipo { get; set; } = default!;
        public DbSet<producto_bancario_usuario> producto_bancario_usuario { get; set; } = default!;
        public DbSet<productobancario> productobancario { get; set; } = default!;
        public DbSet<remesa> remesa { get; set; } = default!;
        public DbSet<tarjeta> tarjeta { get; set; } = default!;
        public DbSet<tarjeta_tipo> tarjeta_tipo { get; set; } = default!;
        public DbSet<transacciones> transacciones { get; set; } = default!;
        public DbSet<usuario> usuario { get; set; } = default!;
        public DbSet<usuario_direccion> usuario_direccion { get; set; } = default!;
        public DbSet<usuario_direccion_tipo> usuario_direccion_tipo { get; set; } = default!;
        public DbSet<usuario_tipo> usuario_tipo { get; set; } = default!;
        public DbSet<zona> zona { get; set; } = default!;
        public DbSet<monedatipocambioapi> monedatipocambioapi { get; set; } = default!;
        public DbSet<producto_bancario_tipo_asignado> producto_bancario_tipo_asignado { get; set; } = default!;
        public DbSet<privado_backend.Models.OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO> OPERACIONES_PRODUCTO_BANCARIO_TIPO_ASIGNADO { get; set; } = default!;
        public DbSet<privado_backend.Models.tipo_cuenta_tarjeta> tipo_cuenta_tarjeta { get; set; } = default!;
        public DbSet<privado_backend.Models.menu> menu { get; set; } = default!;
        public DbSet<privado_backend.Models.usuario_tipo_menu> usuario_tipo_menu { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<menu>(e =>
            {
                e.ToTable("menu");
                e.HasKey(x => x.id);
            });

            mb.Entity<usuario_tipo_menu>(e =>
            {
                e.ToTable("usuario_tipo_menu");
                e.HasKey(x => x.id);
            });
        }
        public DbSet<privado_backend.Models.SistemasPrivado.USUARIOS_TIENDAS> USUARIOS_TIENDAS { get; set; } = default!;
        public DbSet<privado_backend.Models.SistemasPrivado.TRANSACCION_COMPRAS_VENTAS_DETALLE> TRANSACCION_COMPRAS_VENTAS_DETALLE { get; set; } = default!;
        public DbSet<privado_backend.Models.SistemasPrivado.PRODUCTO> PRODUCTO { get; set; } = default!;
        public DbSet<privado_backend.Models.SistemasPrivado.TRANSACCIONES_MOVIMIENTOS> TRANSACCIONES_MOVIMIENTOS { get; set; } = default!;
        public DbSet<privado_backend.Models.SistemasPrivado.INVENTARIO> INVENTARIO { get; set; } = default!;
        public DbSet<privado_backend.Models.SistemasPrivado.TRANSACCIONES_COMPRAS_VENTAS> TRANSACCIONES_COMPRAS_VENTAS { get; set; } = default!;
        public DbSet<privado_backend.Models.SistemasPrivado.TRANSACCIONES> TRANSACCIONES { get; set; } = default!;
        public DbSet<privado_backend.Models.SistemasPrivado.TRANSACCION_TIPO> TRANSACCION_TIPO { get; set; } = default!;
        public DbSet<privado_backend.Models.SistemasPrivado.TIENDAS> TIENDAS { get; set; } = default!;
    }
}
