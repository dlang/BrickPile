﻿/* Copyright (C) 2011 by Marcus Lindblom

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */
using System.Linq;
using System.Web.Mvc;
using BrickPile.Domain.Models;
using BrickPile.Sample.Models;
using BrickPile.Sample.ViewModels;
using Raven.Client;

namespace BrickPile.Sample.Controllers {
    public class HomeController : BaseController<Home> {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            if(CurrentModel != null) {
                var links = new[] {CurrentModel.TeaserOne.Link.Id, CurrentModel.TeaserTwo.Link.Id,CurrentModel.TeaserThree.Link.Id};
                var teasers = DocumentSession.Load<BaseEditorial>(links);
                var viewModel = new HomeViewModel
                                    {
                                        CurrentModel = this.CurrentModel,
                                        Hierarchy = this.Hierarchy,
                                        TeaserOne = teasers.Where(x => x.Id == CurrentModel.TeaserOne.Link.Id).SingleOrDefault(),
                                        TeaserTwo = teasers.Where(x => x.Id == CurrentModel.TeaserTwo.Link.Id).SingleOrDefault(),
                                        TeaserThree = teasers.Where(x => x.Id == CurrentModel.TeaserThree.Link.Id).SingleOrDefault(),
                                        Class = "home",
                                    };

                return View(viewModel);
            }
            return View();
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="session">The session.</param>
        public HomeController(IPageModel model, IDocumentSession session)
            : base(model, session) {
        }
    }
}
