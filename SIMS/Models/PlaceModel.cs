using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.Models
{
    public class PlaceModel
    {
		private Place _place;

		#region Props

		[Display(Name = "ID")]
		//[Required]
		public int Id { get { return _place.Id; } set { _place.Id = value; } }

		[Display(Name = "Place")]
		[Required]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string PlaceName { get { return _place.PlaceName; } set { _place.PlaceName = value; } }

		[Display(Name = "Is Deleted")]
		//[Required]
		public bool Isdeleted { get { return _place.Isdeleted; } set { _place.Isdeleted = value; } }

		public Place Place { get { return _place; } set { _place = value; } }

		#endregion //Props

		#region CTOR

		public PlaceModel()
		{
			_place = new Place();
		}

		public PlaceModel(Place place)
		{
			_place = place;
		}

		#endregion //CTOR
	}
}