﻿using WebSharper.AspNetCore;
using WebSharper.Sitelets;


namespace Axle.Web.WebSharper
{
    public interface ISiteletRegistry
    {
        ISiteletRegistry RegisterSitelet<T>(Sitelet<T> sitelet);
        ISiteletRegistry RegisterSitelet<T>() where T: class, ISiteletService;
    }
}