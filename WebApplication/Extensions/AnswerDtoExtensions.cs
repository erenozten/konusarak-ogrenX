using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication.Service.DTOs;

namespace WebApplication.Extensions
{
    public static class AnswerDtoExtensions
    {
        public static List<SelectListItem> ToSelectListItemList(this List<AnswerDto> answers)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            for (int i = 0; i < answers.Count; i++)
            {
                selectListItems.Add(new SelectListItem($"{i+1}. Şık", i.ToString(), answers[i].IsCorrectAnswer));
            }

            return selectListItems;
        }
    }
}
