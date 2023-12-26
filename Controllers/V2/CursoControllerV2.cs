using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiUniversidade.Model;
using apiUniversidade.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace apiUniversidade.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/{v:apiversion}/curso")]
    public class CursoControllerV2 : ControllerBase
    {
        private readonly ILogger<CursoController> _logger;
        private readonly ApiUniversidadeContext _context;

        public CursoControllerV2(ILogger<CursoController> logger, ApiUniversidadeContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name="GetExemplo")]
        [Route("exemplo")]
        public String GetExemplo()
        {
            return "Api v2";
        }

        [HttpGet]
        public ActionResult<IEnumerable<Curso>> Get()
        {
           var cursos = _context.Cursos.ToList();
           if(cursos is null)
                return NotFound();

            return cursos;
        }

        
        [HttpGet("{id:int}", Name="GetCurso")]
        public ActionResult<Curso> Get(int id)
        {
           var cursos = _context.Cursos.FirstOrDefault(p => p.Id == id);
           if(cursos is null)
                return NotFound("Curso não encontrado.");

            return cursos;
        }


        [HttpPost]
        public ActionResult Post(Curso curso){
            _context.Cursos.Add(curso);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetCurso", 
                new{ id = curso.Id},
                curso);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Curso curso){
            if(id != curso.Id)
                return BadRequest();

            _context.Entry(curso).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(curso);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            var curso = _context.Cursos.FirstOrDefault(p => p.Id == id);

            if(curso is null)
                return NotFound();

            _context.Cursos.Remove(curso);
            _context.SaveChanges();

            return Ok(curso);
        }
    }
}