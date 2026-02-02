
using System.ComponentModel.DataAnnotations;

namespace firstApi.Dto
{
    public class VilaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="نام نمیتواند خالی باشد")]
        [MaxLength(100,ErrorMessage = "نام نمیتواند بیشتر از 100 باشد")]
        public string Name { get; set; }


        [Required(ErrorMessage = " نام شهر نمیتواند خالی باشد")]
        [MaxLength(100, ErrorMessage = "نام شهر نمیتواند  بیشتر از 100  باشد")]
        public string City { get; set; }


        [Required(ErrorMessage = "نام خیابان نمیتواند خالی باشد")]
        [MaxLength(100, ErrorMessage = "نام خیابان نمیتواند  بیشتر از 100  باشد")]
        public string Street { get; set; }


        [Required(ErrorMessage = "آدرس کامل نمیتواند خالی باشد")]
        [MaxLength(500, ErrorMessage = "آدرس کامل نمیتواند  بیشتر از 500  باشد")]
        public string Address { get; set; }


        [Required(ErrorMessage = "موبایل نمیتواند خالی باشد")]
        [MaxLength(11, ErrorMessage = "موبایل نمیتواند بیشتر از 11 باشد")]
        [MinLength(11, ErrorMessage = "موبایل نمیتواند کم تر از 11 باشد")]
        public string Mobile { get; set; }
   
}
}
