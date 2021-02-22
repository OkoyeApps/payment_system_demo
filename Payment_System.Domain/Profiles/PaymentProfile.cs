using AutoMapper;
using Payment_System.Domain.Dtos;
using Payment_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payment_System.Domain.Profiles
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            Func<Payment, PaymentDetailResponse, object> FormatMessage = (src, des) =>
            {
                switch (src.PaymentStatus.Status)
                {
                    case PaymentStatusEnum.PENDING:
                        return "Processing Payment...";
                    case PaymentStatusEnum.PROCESSED:
                        return "Payment Successfully Processed";
                    default:
                        return "Payment Processing Failed";
                }
            };

            CreateMap<Payment_Dto, Payment>()
                .ForMember(dest => dest.CreditCardNumber, opt => opt.MapFrom(src => src.card_number))
                                .ForCtorParam("card_number", x => x.MapFrom(y => y.card_number))
                                .ForCtorParam("card_owner", x => x.MapFrom(y => y.card_holder))
                                .ForCtorParam("expiry_date", x => x.MapFrom(y => y.expiry_date))
                                .ForCtorParam("security_code", x => x.MapFrom(y => y.security_code))
                                .ForCtorParam("amount", x => x.MapFrom(y => y.amount))
                                .ForCtorParam("status", x => x.MapFrom(y => y.Status))
                                .ReverseMap();

            CreateMap<Payment, PaymentDetailResponse>()
                .ForMember(dest => dest.card_holder, opt => opt.MapFrom(src => src.CardHolder))
                .ForMember(dest => dest.card_number, opt => opt.MapFrom(src => src.CreditCardNumber))
                .ForMember(dest => dest.amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.PaymentStatus.Status))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(FormatMessage));

           
        }
    }
}
