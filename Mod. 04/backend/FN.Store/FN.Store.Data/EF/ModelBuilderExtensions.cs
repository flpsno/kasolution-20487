﻿using FN.Store.Domain.Entities;
using FN.Store.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace FN.Store.Data.EF
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoProduto>().HasData(
                    new TipoProduto() { Id = 1, Nome = "Alimento" },
                    new TipoProduto() { Id = 2, Nome = "Higiene" },
                    new TipoProduto() { Id = 3, Nome = "Vestuário" }
                );

            modelBuilder.Entity<Produto>().HasData(
                    new Produto() { Id = 1, Nome = "Picanha", TipoProdutoId = 1, Preco = 90.5M },
                    new Produto() { Id = 2, Nome = "Pasta de Dente Colgate", TipoProdutoId = 2, Preco = 9.5M },
                    new Produto() { Id = 3, Nome = "Fraldas Pampers", TipoProdutoId = 2, Preco = 80.5M },
                    new Produto() { Id = 4, Nome = "Tenis Nike", TipoProdutoId = 3, Preco = 280.5M }
                );

            modelBuilder.Entity<Usuario>().HasData(
                    new Usuario() {Id = 1, Nome = "Administrador", Email = "admin@fansoft.com.br", Senha = "123456".Encrypt() },
                    new Usuario() {Id = 2, Nome = "Usuário", Email = "usuario@fansoft.com.br", Senha = "7890".Encrypt() }
                );
        }

    }
}
