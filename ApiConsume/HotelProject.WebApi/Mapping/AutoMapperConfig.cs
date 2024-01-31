using AutoMapper;
using HotelProject.DtoLayer.Dtos.RoomDto;
using HotelProject.EntityLayer.Concrete;

namespace HotelProject.WebApi.Mapping
{
    public class AutoMapperConfig : Profile
    //Dto ile entityleri bağlayacağımız sınıf
    //Profile sınıfı, AutoMapper konfigürasyonunu tanımlamak için kullanılan bir sınıftır. 

    {
        public AutoMapperConfig()
        {
            CreateMap<RoomAddDto, Room>();
            // CreateMap yöntemi, iki sınıf arasında bir nesne eşlemesi tanımlar.
            // RoomAddDto sınıfından Room sınıfına bir eşleme tanımlanıyor.

            CreateMap<Room, RoomAddDto>();


            CreateMap<UpdateRoomDto, Room>().ReverseMap();
            // CreateMap<Room, UpdateRoomDto>();  ReverseMap sayesinde tekrar bu şekilde kod yazmaya gerek kalmaz tersine de mapleme yapar

        }
    }
}
