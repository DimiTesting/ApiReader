namespace ApiReader.DataModels
{
    public class PlanetPropertiesConversion
    {
        private readonly Planets _planets; 
        public Dictionary<string, int> PlanetDiameter = new Dictionary<string, int>();
        public Dictionary<string, int> PlanetSurface = new Dictionary<string, int>();
        public Dictionary<string, long> PlanetPopulation = new Dictionary<string, long>();

        public void ConvertToDict(Planets _planets)
        {
            int x = 0;
            long z = 0;

            foreach(var planet in _planets.results)
            {
                if(!PlanetDiameter.ContainsKey(planet.name))
                {
                    if(Int32.TryParse(planet.diameter, out x))
                    {
                        PlanetDiameter[planet.name] = x; 
                    }
                    
                }
                if(!PlanetSurface.ContainsKey(planet.name))
                {
                    if(Int32.TryParse(planet.surface_water, out x))
                    {
                        PlanetSurface[planet.name] = x; 
                    }
                    
                }
                if(!PlanetPopulation.ContainsKey(planet.name))
                {
                    if(Int64.TryParse(planet.population, out z))
                    {
                        PlanetPopulation[planet.name] = z; 
                    }
                    
                }
            }
        }

        public void ConvertToDict()
        {
            PlanetDiameter.ToList();
            PlanetSurface.ToList();
            PlanetPopulation.ToList();
        }
    }
}