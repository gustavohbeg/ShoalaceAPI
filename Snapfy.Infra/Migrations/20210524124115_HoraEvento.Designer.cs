﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shoalace.Infra.Contexto;

namespace Shoalace.Infra.Migrations
{
    [DbContext(typeof(ShoalaceContexto))]
    [Migration("20210524124115_HoraEvento")]
    partial class HoraEvento
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Shoalace.Domain.Entities.Acesso", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Alterado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Acesso");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Erro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Parametros")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Erro");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Evento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Alterado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("DiaInteiro")
                        .HasColumnType("bit");

                    b.Property<byte?>("Foto")
                        .HasColumnType("tinyint");

                    b.Property<long?>("GrupoId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Hora")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<string>("Local")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("Notificar")
                        .HasColumnType("int");

                    b.Property<bool>("Publico")
                        .HasColumnType("bit");

                    b.Property<int>("Repetir")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Grupo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Alterado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("Foto")
                        .HasColumnType("tinyint");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Publico")
                        .HasColumnType("bit");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Grupo");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Membro", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Alterado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<long>("GrupoId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Membro");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.MembroEvento", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Alterado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<int>("Comparecer")
                        .HasColumnType("int");

                    b.Property<long>("EventoId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("MembroEvento");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Mensagem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Alterado")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("Audio")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("Foto")
                        .HasColumnType("tinyint");

                    b.Property<long?>("GrupoId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UsuarioDestinoId")
                        .HasColumnType("bigint");

                    b.Property<long>("UsuarioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("UsuarioDestinoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Mensagem");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.StatusMensagem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Alterado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<long>("MembroId")
                        .HasColumnType("bigint");

                    b.Property<long>("MensagemId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MembroId");

                    b.HasIndex("MensagemId");

                    b.ToTable("StatusMensagem");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Usuario", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Alterado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Aniversario")
                        .HasColumnType("datetime2");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Cadastro")
                        .HasColumnType("datetime2");

                    b.Property<byte?>("Foto")
                        .HasColumnType("tinyint");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Visto")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Evento", b =>
                {
                    b.HasOne("Shoalace.Domain.Entities.Grupo", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoId");

                    b.Navigation("Grupo");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Grupo", b =>
                {
                    b.HasOne("Shoalace.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Membro", b =>
                {
                    b.HasOne("Shoalace.Domain.Entities.Grupo", "Grupo")
                        .WithMany("Membros")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoalace.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Grupo");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.MembroEvento", b =>
                {
                    b.HasOne("Shoalace.Domain.Entities.Evento", "Evento")
                        .WithMany("MembrosEvento")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shoalace.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Mensagem", b =>
                {
                    b.HasOne("Shoalace.Domain.Entities.Grupo", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoId");

                    b.HasOne("Shoalace.Domain.Entities.Usuario", "UsuarioDestino")
                        .WithMany()
                        .HasForeignKey("UsuarioDestinoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Shoalace.Domain.Entities.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Grupo");

                    b.Navigation("Usuario");

                    b.Navigation("UsuarioDestino");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.StatusMensagem", b =>
                {
                    b.HasOne("Shoalace.Domain.Entities.Membro", "Membro")
                        .WithMany()
                        .HasForeignKey("MembroId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Shoalace.Domain.Entities.Mensagem", "Mensagem")
                        .WithMany("StatusMensagens")
                        .HasForeignKey("MensagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Membro");

                    b.Navigation("Mensagem");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Evento", b =>
                {
                    b.Navigation("MembrosEvento");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Grupo", b =>
                {
                    b.Navigation("Membros");
                });

            modelBuilder.Entity("Shoalace.Domain.Entities.Mensagem", b =>
                {
                    b.Navigation("StatusMensagens");
                });
#pragma warning restore 612, 618
        }
    }
}
