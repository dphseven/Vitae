using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vitae.Services;

namespace Vitae.Model
{
    public class GeneralInfoRepository : IGeneralInfoRepository
    {
        private IGeneralInfoXMLService xs;

        public GeneralInfoRepository(IGeneralInfoXMLService xmlService) 
        {
            xs = xmlService;
        }

        public Guid Add(IGeneralInfoEntity entity) 
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

        public IGeneralInfoEntity Get(Guid guid) 
        {
            try
            {
                return xs.Get(guid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList<IGeneralInfoEntity> GetAll() 
        {
            try
            {
                return xs.GetAll();
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
                xs.Delete(g);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Guid g, IGeneralInfoEntity t) 
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
