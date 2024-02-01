using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using SIMS.BL;
namespace SIMS.Models
{
    public class GalleryModel
    {
        GalleryBl _gallery;
        
        #region Props
        
        [Display(Name = "ID")]
        public int Id { get { return _gallery.Id; } set { _gallery.Id = value; } }
        
        [Display(Name = "File Name")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FileName { get { return _gallery.FileName; } set { _gallery.FileName = value; } }
        
        [Display(Name = "Photo")]
        public string FileData { get { return _gallery.FileData; } set { _gallery.FileData = value; } }

        [Display(Name = "Thumbnail")]
        public string Thumbnail { get { return _gallery.Thumbnail; } set { _gallery.Thumbnail = value; } }
        
        [Display(Name = "File Type")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FileType { get { return _gallery.FileType; } set { _gallery.FileType = value; } }
        
        [Display(Name = "Title")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Title { get { return _gallery.Title; } set { _gallery.Title = value; } }
        
        [Display(Name = "Discription")]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FileDiscription { get { return _gallery.FileDiscription; } set { _gallery.FileDiscription = value; } }
        public GalleryBl Gallery { get { return _gallery; } set { _gallery = value; } }
        public string FileDataOldData
        {
            get { return _gallery.FileDataOldData; }
            set { _gallery.FileDataOldData = value; }
        }
        public string ThumbnailOldData
        {
            get { return _gallery.ThumbnailOldData; }
            set { _gallery.ThumbnailOldData = value; }
        }
        #endregion //Props

        #region CTOR

        public GalleryModel()
        {
            _gallery = new GalleryBl();
        }
        
        public GalleryModel(GalleryBl gallery)
        {
            _gallery = gallery;
        }
        
        #endregion //CTOR
        
    }
}