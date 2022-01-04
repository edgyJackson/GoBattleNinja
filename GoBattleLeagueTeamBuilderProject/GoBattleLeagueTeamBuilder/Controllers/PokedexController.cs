using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoBattleLeagueTeamBuilder.Models;
using GoBattleLeagueTeamBuilder.Models.Interfaces;

namespace GoBattleLeagueTeamBuilder.Controllers
{
    public class PokedexController : Controller
    {
        private readonly GoBattleLeagueTeamBuilderDBContext _context;
        private readonly IRepository<Pokedex> _repo;
        private readonly IPVP_IVsAPIRepository _pvpIVAPI;

        public PokedexController(GoBattleLeagueTeamBuilderDBContext context, IRepository<Pokedex> repo, IPVP_IVsAPIRepository pvpIVAPI)
        {
            _context = context;
            _repo = repo;
            _pvpIVAPI = pvpIVAPI;
        }

        // GET: Pokedex
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pokedexes.ToListAsync());
        }

      
        // GET: Pokedex/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokedex = await _context.Pokedexes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokedex == null)
            {
                return NotFound();
            }

            return View(pokedex);
        }

        // GET: Pokedex/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pokedex/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PokemonId,Name,Form,BaseAtk,BaseDef,BaseSta,LlatkIv,LldefIv,LlstaIv,Lllevel,LlcP,LlatkStat,LldefStat,LlstaStat,LlstatProduct,GlatkIv,GldefIv,GlstaIv,Gllevel,GlcP,GlatkStat,GldefStat,GlstaStat,GlstatProduct,UlatkIv,UldefIv,UlstaIv,Ullevel,UlcP,UlatkStat,UldefStat,UlstaStat,UlstatProduct,UlclassicatkIv,UlclassicdefIv,UlclassicstaIv,Ulclassiclevel,UlclassiccP,UlclassicatkStat,UlclassicdefStat,UlclassicstaStat,UlclassicstatProduct")] Pokedex pokedex)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pokedex);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pokedex);
        }

        // GET: Pokedex/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokedex = await _context.Pokedexes.FindAsync(id);
            if (pokedex == null)
            {
                return NotFound();
            }
            return View(pokedex);
        }

        // POST: Pokedex/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PokemonId,Name,Form,BaseAtk,BaseDef,BaseSta,LlatkIv,LldefIv,LlstaIv,Lllevel,LlcP,LlatkStat,LldefStat,LlstaStat,LlstatProduct,GlatkIv,GldefIv,GlstaIv,Gllevel,GlcP,GlatkStat,GldefStat,GlstaStat,GlstatProduct,UlatkIv,UldefIv,UlstaIv,Ullevel,UlcP,UlatkStat,UldefStat,UlstaStat,UlstatProduct,UlclassicatkIv,UlclassicdefIv,UlclassicstaIv,Ulclassiclevel,UlclassiccP,UlclassicatkStat,UlclassicdefStat,UlclassicstaStat,UlclassicstatProduct")] Pokedex pokedex)
        {
            if (id != pokedex.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokedex);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokedexExists(pokedex.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pokedex);
        }

        // GET: Pokedex/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokedex = await _context.Pokedexes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokedex == null)
            {
                return NotFound();
            }

            return View(pokedex);
        }

        // POST: Pokedex/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokedex = await _context.Pokedexes.FindAsync(id);
            _context.Pokedexes.Remove(pokedex);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokedexExists(int id)
        {
            return _context.Pokedexes.Any(e => e.Id == id);
        }
    }
}
