using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitae.Services;

namespace Vitae.Model
{
    public class ExpertiseRepository : IExpertiseRepository
    {
        IXMLService xs;

        public ExpertiseRepository(IXMLService xmlService) 
        {
            xs = xmlService;
        }

        public Guid Add(IExpertiseEntity entity) 
        {
            try
            {
                return xs.Insert(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IExpertiseEntity Get(Guid guid) 
        {
            try
            {
                return xs.GetExpertiseItem(guid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<IExpertiseEntity> GetAll() 
        {
            try
            {
                return xs.GetExpertise();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Remove(Guid g) 
        {
            try
            {
                xs.DeleteExpertise(g);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Guid g, IExpertiseEntity t) 
        {
            try
            {
                xs.Update(g, t);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
