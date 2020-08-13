﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorCartoesCredito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCartoesCredito.Controllers
{
    public class GastosController : Controller
    {
        private readonly Contexto _contexto;

        public GastosController(Contexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IActionResult> ListagemGastos(int cartaoId)
        {
            Cartao cartao = await _contexto.Cartoes.FindAsync(cartaoId);
            double soma = await _contexto.Gastos.Where(c => c.CartaoId == cartaoId).SumAsync(c => c.Valor);

            GastosViewModel gastosViewModel = new GastosViewModel
            {
                CartaoId = cartaoId,
                NumeroCartao = cartao.NumeroCartao,
                ListaGastos = await _contexto.Gastos.Where(c => c.CartaoId == cartaoId).ToListAsync(),
                PorcentagemGasta = Convert.ToInt32((soma / cartao.Limite) * 100)
            };

            return View(gastosViewModel);
        }

        [HttpGet]
        public IActionResult NovoGasto(int cartaoId)
        {
            Gasto gasto = new Gasto
            {
                CartaoId = cartaoId
            };

            return View(gasto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NovoGasto(Gasto gasto)
        {
            if (ModelState.IsValid)
            {
                await _contexto.AddAsync(gasto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(ListagemGastos), new { cartaoId = gasto.CartaoId });
            }

            return View(gasto);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarGasto(int gastoId)
        {
            Gasto gasto = await _contexto.Gastos.FindAsync(gastoId);
            
            if (gasto == null)
                return NotFound();

            return View(gasto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarGasto(int gastoId, Gasto gasto)
        {
            if (gastoId != gasto.GastoId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _contexto.Update(gasto);
                await _contexto.SaveChangesAsync();
                return RedirectToAction(nameof(ListagemGastos), new { cartaoId = gasto.CartaoId });
            }

            return View(gasto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirGasto(int gastoId)
        {
            Gasto gasto = await _contexto.Gastos.FindAsync(gastoId);
            _contexto.Remove(gasto);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(ListagemGastos), new { cartaoId = gasto.CartaoId });
        }
        
    }
}
