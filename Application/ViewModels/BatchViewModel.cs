using Application.Interfaces;
using AutoMapper;
using Domain.Entites;
using System.Collections.Generic;

namespace Application.ViewModels
{
    public class BatchViewModel : BaseViewModel, IMapFrom<Batch>
    {
        public string Key { get; set; }
        public ProductViewModel Product { get; set; }

     
    }
}