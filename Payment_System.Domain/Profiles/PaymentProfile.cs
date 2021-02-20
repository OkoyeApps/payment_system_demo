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
            CreateMap<Payment_Dto, Payment>()
                .ForMember(dest => dest.CreditCardNumber, opt => opt.MapFrom(src => src.card_number))
                                .ForMember(dest => dest.CreditCardNumber, opt => opt.MapFrom(src => src.card_number))
                                .ForMember(dest => dest.CardHolder, opt => opt.MapFrom(src => src.card_holder))
                                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.expiry_date))
                                .ForMember(dest => dest.SecurityCode, opt => opt.MapFrom(src => src.security_code))
                                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.amount))
                                .ReverseMap();
        }
    }
}
