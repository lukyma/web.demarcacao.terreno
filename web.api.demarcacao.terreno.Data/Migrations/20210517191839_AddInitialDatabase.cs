using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace web.api.demarcacao.terreno.Data.Migrations
{
    public partial class AddInitialDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPREENDIMENTO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DESCRICAO = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    GRUPO_EMPRESA = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    ENDERECO_LOGRADOURO = table.Column<string>(type: "text", nullable: true),
                    ENDERECO_NUMERO = table.Column<string>(type: "text", nullable: true),
                    ENDERECO_COMPLEMENTO = table.Column<string>(type: "text", nullable: true),
                    ENDERECO_BAIRRO = table.Column<string>(type: "text", nullable: true),
                    ENDERECO_CIDADE = table.Column<string>(type: "text", nullable: true),
                    ENDERECO_ESTADO = table.Column<string>(type: "text", nullable: true),
                    ENDERECO_CEP = table.Column<string>(type: "text", nullable: true),
                    ENDERECO_REFERENCIA = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPREENDIMENTO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "INTERFACE",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DESCRICAO = table.Column<string>(type: "text", nullable: false),
                    TAG = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INTERFACE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NOME = table.Column<string>(type: "text", nullable: false),
                    LOGIN = table.Column<string>(type: "text", nullable: false),
                    PASSWORD = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TERRENO",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_EMPREENDIMENTO = table.Column<long>(type: "bigint", nullable: false),
                    DESCRICAO = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TERRENO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TERRENO_EMPREENDIMENTO_ID_EMPREENDIMENTO",
                        column: x => x.ID_EMPREENDIMENTO,
                        principalTable: "EMPREENDIMENTO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO_INTERFACE",
                columns: table => new
                {
                    ID_USUARIO = table.Column<long>(type: "bigint", nullable: false),
                    ID_INTERFACE = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO_INTERFACE", x => new { x.ID_USUARIO, x.ID_INTERFACE });
                    table.ForeignKey(
                        name: "FK_USUARIO_INTERFACE_INTERFACE_ID_INTERFACE",
                        column: x => x.ID_INTERFACE,
                        principalTable: "INTERFACE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USUARIO_INTERFACE_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "COORDENADA",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ID_TERRENO = table.Column<long>(type: "bigint", nullable: false),
                    ORDEM = table.Column<int>(type: "integer", nullable: false),
                    LONGITUDE = table.Column<decimal>(type: "numeric", nullable: false),
                    LATITUDE = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COORDENADA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COORDENADA_TERRENO_ID_TERRENO",
                        column: x => x.ID_TERRENO,
                        principalTable: "TERRENO",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "INTERFACE",
                columns: new[] { "ID", "DESCRICAO", "TAG" },
                values: new object[,]
                {
                    { 1L, "Cadastro de Empreendimento", "CAD_EMP" },
                    { 2L, "Atualização de Empreendimento", "ATUAL_EMP" },
                    { 3L, "Exclusão de Empreendimento", "EXC_EMP" },
                    { 4L, "Listagem de Empreendimento", "LIST_EMP" },
                    { 5L, "Cadastro de Terreno", "CAD_TERR" },
                    { 6L, "Atualização de Terreno", "ATUAL_TERR" },
                    { 7L, "Exclusão de Terreno", "EXC_TERR" },
                    { 8L, "Listagem de Terreno", "LIST_TERR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_COORDENADA_ID_TERRENO",
                table: "COORDENADA",
                column: "ID_TERRENO");

            migrationBuilder.CreateIndex(
                name: "IX_TERRENO_ID_EMPREENDIMENTO",
                table: "TERRENO",
                column: "ID_EMPREENDIMENTO");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_INTERFACE_ID_INTERFACE",
                table: "USUARIO_INTERFACE",
                column: "ID_INTERFACE");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COORDENADA");

            migrationBuilder.DropTable(
                name: "USUARIO_INTERFACE");

            migrationBuilder.DropTable(
                name: "TERRENO");

            migrationBuilder.DropTable(
                name: "INTERFACE");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "EMPREENDIMENTO");
        }
    }
}
