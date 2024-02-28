using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermercadoMDDII.Models
{
    public partial class SupermercadoContext : DbContext
    {
        public SupermercadoContext()
        {
        }
        public SupermercadoContext(DbContextOptions<SupermercadoContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ClienteModel> Clientes { get; set; }
        public virtual DbSet<CategoriaModel> Categorias { get; set; }
        public virtual DbSet<ProductoModel> Productos { get; set; }

        public virtual DbSet<ConfiguracionModel> Configuracions { get; set; }
        public virtual DbSet<NegocioModel> Negocios { get; set; }

        public virtual DbSet<AsistenciaModel> Asistencias { get; set; }
        public virtual DbSet<DetalleVentaModel> DetalleVentas { get; set; }
        public virtual DbSet<EstadoPedidoModel> EstadoPedidos { get; set; }
        public virtual DbSet<PedidoModel> Pedidos { get; set; }
        public virtual DbSet<ProveedorModel> Proveedors { get; set; }
        public virtual DbSet<UsuarioModel> Usuarios { get; set; }
        public virtual DbSet<VentaModel> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BaseSupermercado;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}