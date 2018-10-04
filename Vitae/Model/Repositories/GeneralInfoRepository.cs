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
        private IXMLService xs;

        public GeneralInfoRepository(IXMLService xmlService) 
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
                return xs.GetGeneralInformation(guid);
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
                return new List<IGeneralInfoEntity>
                {
                    xs.GetGeneralInformation(Guid.Empty)
                };
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
                xs.DeleteGeneralInfo(g);
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
