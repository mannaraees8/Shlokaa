using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.Models
{
    public class SearchModel
    {
        MaterialMovement _materialMovement;

        [Display(Name = "ID")]
        public int Id { get { return _materialMovement.Id; } set { _materialMovement.Id = value; } }


        [Display(Name = "Name")]
        [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string MovementType { get { return _materialMovement.Movementtype; } set { _materialMovement.Movementtype = value; } }



        #region CTOR

        public SearchModel()
        {
            _materialMovement = new MaterialMovement();
        }

        public SearchModel(MaterialMovement materialMovement)
        {
            _materialMovement = materialMovement;
        }

        #endregion //CTOR

    }
}