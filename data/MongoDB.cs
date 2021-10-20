using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using Api.data.Collections;

namespace Api.data
{
    public class MongoDB
    {
        public IMongoDatabase DB{get;}
        public MongoDB(IConfiguration configuration)
        {
            try 
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB= client.GetDatabase(configuration["NomeBanco"]);
                MapClasses();
            }
            catch (Exception Ex)
            {
                throw new MongoException("It was not possible to connect to Mongo DB",Ex);
            }

        } 
        public void MapClasses()
        {
            var conventionPack = new ConventionPack{new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase",conventionPack,t=>true);
            if(!BsonClassMap.IsClassMapRegistered(typeof(Infectado)))
            {
                BsonClassMap.RegisterClassMap<Infectado>(i=>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);

                });
            }
            
        }
    }
}