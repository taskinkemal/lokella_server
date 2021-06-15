﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.TransferObjects;
using WebCommon.BaseControllers;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : NoAuthController
    {
        private readonly IMenuManager menuManager;

        public MenuController(IMenuManager menuManager)
        {
            this.menuManager = menuManager;
        }

        [HttpGet]
        [Route("/Menu/Businesses/{id}")]
        public async Task<List<MenuCategory>> GetMenuCategories(int id)
        {
            return await menuManager.GetBusinessMenuCategories(id);
        }

        [HttpGet]
        [Route("/Menu/Items/Category/{id}")]
        public async Task<List<MenuItemTo>> GetMenuItems(int id)
        {
            return await menuManager.GetMenuItems(id);
        }


    }
}
