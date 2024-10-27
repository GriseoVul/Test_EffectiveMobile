using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SortOrders.API.Models;

public class Order
{
    public int Id {get; set;} = 0;
    public double Weight {get;set;} = 0.0f;
    public string District{get; set;} = string.Empty;
    public DateTime DeliveryTime {get;set;} = DateTime.MinValue;

    public void Validate()
    {
        if(Id <= 0) throw new ValidationException($"Id can't be less or equal zero: Id= {Id}");
        if(Weight <= 0) throw new ValidationException($"Weight can't be less or equal zero: Weight= {Weight}");
        if(District == String.Empty) throw new ValidationException($"District name cant be empty: District= {Weight}");
        if(DeliveryTime <= DateTime.MinValue && DeliveryTime >= DateTime.MaxValue) 
            throw new ValidationException($"Wrong DeliveryTime value: DeliveryTime= {Weight}");
    }
    public string ToCsv()
    {
        return $"{Id},{Weight.ToString(CultureInfo.InvariantCulture)},{District},{DeliveryTime}";
    }
    public static Order FromCsv(string csvLine)
    {
        var values = csvLine.Split(',');
        
        if (values.Length != 4)
        {
            throw new FormatException("Invalid CSV format: incorrect number of columns");
        }

        var result = new Order();
        result.Id = Convert.ToInt32(values[0]);
        result.Weight = Convert.ToDouble(values[1], CultureInfo.InvariantCulture);
        result.District = values[2];
        result.DeliveryTime = Convert.ToDateTime(values[3]);
        
        result.Validate();
        
        return result;
    }
}