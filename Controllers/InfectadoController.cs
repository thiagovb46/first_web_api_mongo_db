using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.data.Collections;
using Api.data.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfectadoController : ControllerBase
    {
        data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;
        public InfectadoController(data.MongoDB mongoDB) 
        {
            _mongoDB = mongoDB;
            _infectadosCollection= _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());

        }
        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto) 
        {
            var infectado  = new Infectado(dto.DataNascimento,dto.Sexo,dto.Latitude,dto.Longitude);
            _infectadosCollection.InsertOne(infectado);
            return StatusCode(201, "Infectado adicionado com sucesso");
        }
        [HttpGet] 
        public ActionResult ObterInfectados() 
        {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }
        
    }
}