using Application.DTOs.RequestDTOs.IO;
using Application.DTOs.RequestDTOs.LCD;
using Application.DTOs.RequestDTOs.LED;
using Application.DTOs.ResponseDTOs;
using Application.DTOs.ResponseDTOs.IO;
using Application.DTOs.ResponseDTOs.LCD;
using Application.DTOs.ResponseDTOs.LED;
using AutoMapper;
using Domain.Enitties;
using Domain.Enitties.IO;
using Domain.Enitties.LCD;
using Domain.Enitties.LED;
using Domain.Entities.LED;
using Infrastructure.Repositories.IO;
namespace Infrastructure.ExternalServices.Mapper
{
    public class ApplicationMapper:Profile
    {
        public ApplicationMapper() 
        {
            CreateMap<LedResult, LedResultRequest>();
            CreateMap<LedResultRequest, LedResult>();
            CreateMap<LedResult, LedResultResponse>();
            CreateMap<LedResultResponse, LedResult>();
            CreateMap<LedDeviceStatusRequest, LedDeviceStatus>();
            CreateMap<LedDeviceStatus, LedDeviceStatusRequest>();
            CreateMap<LedDeviceStatus, LedDeviceStatusResponse>();
            CreateMap<LedDeviceStatusResponse, LedDeviceStatus>();
            CreateMap<LED, LEDDTO>();
            CreateMap<LED, LedResponse>();
            CreateMap<LedResponse, LED>();
            CreateMap<Job, JobDTO>();
            CreateMap<LedCamera, LedCameraDTO>();
            CreateMap<LedModel, LedModelDTO>();
            CreateMap<LedModelConfig, LedModelConfigDTO>();
            CreateMap<LedStatus, LedStatusDTO>();
            CreateMap<Line, LineDTO>();
            CreateMap<LedConfig, LedConfigDTO>();
            CreateMap<LedConfig, LedConfigResponse>();
            CreateMap<LedConfig, LedConfigResponse>()
            
            // Map cameras by combining value and status
            .ForMember(dest => dest.Camera1, opt => opt.MapFrom(src => CombineWithUnderscore(src.Camera1, src.Camera1_Status)))
            .ForMember(dest => dest.Camera2, opt => opt.MapFrom(src => CombineWithUnderscore(src.Camera2, src.Camera2_Status)))
            .ForMember(dest => dest.Camera3, opt => opt.MapFrom(src => CombineWithUnderscore(src.Camera3, src.Camera3_Status)))
            .ForMember(dest => dest.Camera4, opt => opt.MapFrom(src => CombineWithUnderscore(src.Camera4, src.Camera4_Status)))
            .ForMember(dest => dest.Camera5, opt => opt.MapFrom(src => CombineWithUnderscore(src.Camera5, src.Camera5_Status)))
            // Map SaftyPosition from X,Y,Z coordinates
            .ForMember(dest => dest.SaftyPosition, opt => opt.MapFrom(src => CombineWithUnderscore(src.X_SaftyPosition, src.Y_SaftyPosition, src.Z_SaftyPosition)))
            // Scanner is ignored (no source)
            .ForMember(dest => dest.Scanner, opt => opt.Ignore())
            // All other properties with same name are mapped automatically
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Line, LineModelResponse>();

            //reverse mapping
            CreateMap<LEDDTO, LED>();
            CreateMap<JobDTO, Job>();
            CreateMap<LedCameraDTO, LedCamera>();
            CreateMap<LedModelDTO, LedModel>();
            CreateMap<LedModelConfigDTO, LedModelConfig>();
            CreateMap<LedStatusDTO, LedStatus>();
            CreateMap<LineDTO, Line>();
            CreateMap<LedConfigDTO, LedConfig>();

            /// <summary>
            /// Mapping for LCD
            /// </summary>
            CreateMap<LCDModel, ResponseLCDModel>();
            CreateMap<ResponseLCDModel, LCDModel>();
            CreateMap<LCDModel, RequestLCDModel>();
            CreateMap<RequestLCDModel, LCDModel>();
            CreateMap<LCDConfig, ResponseLCDConfig>();
            CreateMap<ResponseLCDConfig, LCDConfig>();
            CreateMap<LCDConfig, RequestLCDConfig>();
            CreateMap<RequestLCDConfig, LCDConfig>();
            CreateMap<RequestLCDResult, LCDResult>();
            CreateMap<LCDResult, RequestLCDResult>();
            CreateMap<ResponseLCDResult, LCDResult>();
            CreateMap<LCDResult, ResponseLCDResult>();

            ///<summary>
            ///Mapping for IO
            ///</summary>
            CreateMap<IOModel, IOModelResponse>();
            CreateMap<IOModelRequest, IOModel>();
            CreateMap<IOConfigManagement, IOConfigResponse>();
            CreateMap<IOConfigDTO, IOConfig>();
            CreateMap<IOConfig, IOConfigDTO>();
            CreateMap<IOConfigRequest, IOConfigManagement>();
            CreateMap<IOMotionItemDTO, MotionPoint>();
            CreateMap<MotionPoint, IOMotionItemDTO>();
            CreateMap<IOMotionPointsRequest, MotionPointsManagement>();
            CreateMap<MotionPointsManagement, IOMotionPointsResponse>();
            CreateMap<IOOffsetDTO, Offset>();
            CreateMap<Offset,IOOffsetDTO>();
            CreateMap<IOOffsetsRequest, OffsetManagement>();
            CreateMap<OffsetManagement, IOOffsetsResponse>();
            CreateMap<IOPressureRequest,PressureManagement>();
            CreateMap<PressureManagement,IOPressureResponse>();
            CreateMap<PressureItem, PressureItemDTO>();
            CreateMap<PressureItemDTO, PressureItem>();
        }
        private static string CombineWithUnderscore(params string?[] parts)
        {
            var nonEmpty = parts.Where(p => !string.IsNullOrEmpty(p)).ToArray();
            return nonEmpty.Length == 0 ? null : string.Join("_", nonEmpty);
        }
    }
}
