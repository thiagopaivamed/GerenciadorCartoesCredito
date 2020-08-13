﻿// <auto-generated />
using GerenciadorCartoesCredito.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GerenciadorCartoesCredito.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20200614141140_CriacaoBancoDados")]
    partial class CriacaoBancoDados
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GerenciadorCartoesCredito.Models.Cartao", b =>
                {
                    b.Property<int>("CartaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Limite")
                        .HasColumnType("float");

                    b.Property<string>("NomeBanco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroCartao")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CartaoId");

                    b.ToTable("Cartoes");
                });

            modelBuilder.Entity("GerenciadorCartoesCredito.Models.Gasto", b =>
                {
                    b.Property<int>("GastoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CartaoId")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("GastoId");

                    b.HasIndex("CartaoId");

                    b.ToTable("Gastos");
                });

            modelBuilder.Entity("GerenciadorCartoesCredito.Models.Gasto", b =>
                {
                    b.HasOne("GerenciadorCartoesCredito.Models.Cartao", "Cartao")
                        .WithMany("Gastos")
                        .HasForeignKey("CartaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
