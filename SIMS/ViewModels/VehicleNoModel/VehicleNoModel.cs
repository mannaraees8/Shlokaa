using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.ViewModels.VehicleNoModel
{
    public class VehicleNoModel
    {
		private VehicleNo _vehicleNo;
		private List<VehicleNo> lstVehicleNo;

		#region Props

		[Display(Name = "ID")]
		//[Required]
		public int Id { get { return _vehicleNo.Id; } set { _vehicleNo.Id = value; } }

		[Display(Name = "VehicleNo")]
		[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string VehicleNum { get { return _vehicleNo.VehicleNum; } set { _vehicleNo.VehicleNum = value; } }

		[Display(Name = "IsDeleted")]
		//[Required]
		public bool Isdeleted { get { return _vehicleNo.Isdeleted; } set { _vehicleNo.Isdeleted = value; } }

		public VehicleNo VehicleNoObj { get { return _vehicleNo; } set { _vehicleNo = value; } }
		public List<VehicleNo> VehicleNoList { get { return lstVehicleNo; } set { lstVehicleNo = value; } }

		#endregion //Props

		#region CTOR

		public VehicleNoModel()
		{
			_vehicleNo = new VehicleNo();
		}

		public VehicleNoModel(VehicleNo vehicleNo)
		{
			_vehicleNo = vehicleNo;
		}
		public VehicleNoModel(List<VehicleNo> vehicleNoList)
		{
			_vehicleNo = new VehicleNo();
			lstVehicleNo = vehicleNoList;
		}

		public VehicleNoModel(List<VehicleNo> vehicleNoList, VehicleNo vehicleNo)
		{
			_vehicleNo = vehicleNo;
			lstVehicleNo = vehicleNoList;
		}
		#endregion //CTOR
	}
}