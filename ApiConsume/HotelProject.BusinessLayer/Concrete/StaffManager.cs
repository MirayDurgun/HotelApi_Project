using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelProject.BusinessLayer.Abstract;
using HotelProject.DataAccessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;

namespace HotelProject.BusinessLayer.Concrete
{
	public class StaffManager : IStaffService
	{
		private readonly IStaffDal _staffDal;
		public List<Staff> GetList()
		{
			return _staffDal.GetList();
		}

		public void TDelete(Staff t)
		{
			_staffDal.Delete(t);
		}

		public Staff TGetByID(int id)
		{
			return _staffDal.GetByID(id);
		}

		public void TInsert(Staff t)
		{
			_staffDal.Insert(t);
		}

		public void TUpdate(Staff t)
		{
			_staffDal.Update(t);
		}
	}
}
