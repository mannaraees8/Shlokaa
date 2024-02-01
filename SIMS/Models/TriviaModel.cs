using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;

namespace SIMS.Models
{
    public class TriviaModel
    {
        private Trivia _trivia;


        [Display(Name = "ID")]
        public int Id { get { return _trivia.Id; } set { _trivia.Id = value; } }


        [Display(Name = "TabName")]
      //  [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string TabName { get { return _trivia.TabName; } set { _trivia.TabName = value; } }

        [Display(Name = "Contents")]
    //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Contents { get { return _trivia.Contents; } set { _trivia.Contents = value; } }

        [Display(Name = "Title")]
  //      [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get { return _trivia.Title; } set { _trivia.Title = value; } }



        [Display(Name = "SubContents")]
    //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string SubContents { get { return _trivia.SubContents; } set { _trivia.SubContents = value; } }

        [Display(Name = " Point1")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point1 { get { return _trivia.Point1; } set { _trivia.Point1 = value; } }


        [Display(Name = " Point2")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point2 { get { return _trivia.Point2; } set { _trivia.Point2 = value; } }

        [Display(Name = " Point3")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point3 { get { return _trivia.Point3; } set { _trivia.Point3 = value; } }

        [Display(Name = " Point4")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point4 { get { return _trivia.Point4; } set { _trivia.Point4 = value; } }

        [Display(Name = " Point5")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point5 { get { return _trivia.Point5; } set { _trivia.Point5= value; } }

        [Display(Name = " Point6")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point6 { get { return _trivia.Point6; } set { _trivia.Point6 = value; } }

        [Display(Name = " Point7")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point7 { get { return _trivia.Point7; } set { _trivia.Point7= value; } }

        [Display(Name = " Point8")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point8 { get { return _trivia.Point8; } set { _trivia.Point8 = value; } }

        [Display(Name = " Point9")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point9{ get { return _trivia.Point9; } set { _trivia.Point9 = value; } }


        [Display(Name = " Point10")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Point10 { get { return _trivia.Point10; } set { _trivia.Point10 = value; } }

        [Display(Name = " Type")]
        //    [Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string Type { get { return _trivia.Type; } set { _trivia.Type = value; } }

        [Display(Name = "Image")]
        public byte[] Image { get { return _trivia.Image; } set { _trivia.Image = value; } }

        [Display(Name = "IsDeleted")]
        //[Required]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public bool IsDeleted { get { return _trivia.Isdeleted; } set { _trivia.Isdeleted = value; } }

        public Trivia Trivia { get { return _trivia; } set { _trivia = value; } }


        #region CTOR

        public TriviaModel()
        {
            _trivia = new Trivia();
        }

        public TriviaModel(Trivia trivia)
        {
            _trivia = trivia;
        }

        #endregion //CTOR
    }
}