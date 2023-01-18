﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Pages.Basket;

[Authorize]
public class SuccessModel : PageModel
{
    private IBasketViewModelService _basketViewModelService;

    public SuccessModel(IBasketViewModelService basketViewModelService)
    {
        _basketViewModelService = basketViewModelService;
    }

    public void OnGet()
    {
        string username = Request.HttpContext.User.Identity.Name!;

        _basketViewModelService.ReserveBasketItems(username);
    }
}
