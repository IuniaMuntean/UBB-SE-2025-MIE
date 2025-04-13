using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Iunia_Fabi.Model;
using App1.Iunia_Fabi.Data;
using System.Reflection.Metadata.Ecma335;
using System.Xml.Linq;

namespace App1.Iunia_Fabi.Service
{
    internal class GraphService
    {
        private CityDBContext? _dbCityContext = new CityDBContext();
        private RoadDBContext? _roadDBContext = new();
        public Graph? Graph = new Graph();

        public GraphService()
        {
            var dbCities = _dbCityContext.Cities.ToList();
            foreach (City city in dbCities) {
                Graph.add(city);
            }

            var dbRoads = _roadDBContext.Roads.ToList();
            foreach (Road road in dbRoads) {
                Graph.add(road);
            }
        }

        public List<City> ListCities()
        {
            return Graph.Cities();
        }

        public List<Road> ListRoads()
        {
            return Graph.Roads();
        }

        public City GetCityAtID(int id)
        {
            return Graph.City(id);
        }

        public void InsertCityDB(string name, int x, int y)
        {
            int id = 1;
            try
            {
                id = _dbCityContext.Cities
                .OrderBy(c => c.id)
                .Last().id + 1;
            }
            catch {  }
            City c = new City(id, name, x, y);
            Graph.add(c);
            _dbCityContext.Add(c);
            _dbCityContext.SaveChanges();
        }

        public void InsertRoadDB(int idStartCity,  int idEndCity, int value)
        {
            Road r = new Road(idStartCity, idEndCity, value);
            Graph.add(r);
            _roadDBContext.Add(r);
            _roadDBContext.SaveChanges(true);
        }

        
    }
}
