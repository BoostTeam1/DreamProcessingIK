using Business.Abstract;
using Business.Constants;
using Core.Unilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private ICompanyDal _companyDal;
        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }
        public void Add(Company company)
        {

            _companyDal.Add(company);
            //return new SuccessResult(Messages.CompanyAdded);
        }

       

        public void Delete(Company company)
        {
            _companyDal.Delete(company);
            //return new SuccessResult(Messages.CompanyDeleted);
        }

        public Company GetById(int companyId)
        {
            //return new SuccessDataResult<Company>(_companyDal.Get(filter: x => x.Id == companyId).Result);
            return _companyDal.Get(filter: x => x.Id == companyId).Result;
        }

        public List<Company> GetList()
        {
            //return new SuccessDataResult<List<Company>>(_companyDal.GetList().Result.ToList());
            return _companyDal.GetList().Result.ToList();
        }

        

        public void Update(Company company)
        {
            _companyDal.Update(company);
            //return new SuccessResult(Messages.CompanyUpdated);

        }
    }
}
