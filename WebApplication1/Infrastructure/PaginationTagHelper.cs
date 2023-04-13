using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using IntexWinter.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntexWinter.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        // Dynamically create page links
        private IUrlHelperFactory uhf;

        public PaginationTagHelper (IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public BurialsViewModel PageModel { get; set; }
        public string PageAction { get; set; }
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassEnabled { get; set; }
        public string PageClassDisabled { get; set; }
        public string PageClassSelected { get; set; }



        public override void Process (TagHelperContext thc, TagHelperOutput tho)
        {
            string Sex;
            if (PageModel.PageInfo.SelectedSex == "z") {Sex = "";} else {Sex = PageModel.PageInfo.SelectedSex;}

            string HairColor;
            if (PageModel.PageInfo.SelectedHairColor == "z") { HairColor = ""; } else { HairColor = PageModel.PageInfo.SelectedHairColor; }

            string AgeAtDeath;
            if (PageModel.PageInfo.SelectedAgeAtDeath == "z") { AgeAtDeath = ""; } else { AgeAtDeath = PageModel.PageInfo.SelectedAgeAtDeath; }

            string HeadDirection;
            if (PageModel.PageInfo.SelectedHeadDir == "z") { HeadDirection = ""; } else { HeadDirection = PageModel.PageInfo.SelectedHeadDir; }

            string Depth;
            if (PageModel.PageInfo.SelectedDepth == "z") { Depth = ""; } else { Depth = PageModel.PageInfo.SelectedDepth; }

            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");


            TagBuilder first = new TagBuilder("a");
            TagBuilder prev = new TagBuilder("a");
            TagBuilder currentLess2 = new TagBuilder("a");
            TagBuilder currentLess1 = new TagBuilder("a");
            TagBuilder current = new TagBuilder("a");
            TagBuilder currentPlus1 = new TagBuilder("a");
            TagBuilder currentPlus2 = new TagBuilder("a");
            TagBuilder next = new TagBuilder("a");
            TagBuilder last = new TagBuilder("a");

            var currentPage = PageModel.PageInfo.CurrentPage;

            // Enabled buttons if the current page is more than 1
            if (currentPage > 1)
            {
                first.Attributes["href"] = uh.Action(PageAction, new { pageNum = 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                first.AddCssClass(PageClass); first.AddCssClass(PageClassEnabled);
                first.InnerHtml.Append("First");

                prev.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage - 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                prev.AddCssClass(PageClass); prev.AddCssClass(PageClassEnabled);
                prev.InnerHtml.Append("Prev");

                //Enabled button if the current page is more than 2
                if (currentPage > 2)
                {
                    currentLess2.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage - 2, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                    currentLess2.AddCssClass(PageClass); currentLess2.AddCssClass(PageClassEnabled);
                    currentLess2.InnerHtml.Append((currentPage - 2).ToString());
                }
                else // Disabled buttons if current page is 2
                {
                    currentLess2.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage - 2, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                    currentLess2.AddCssClass(PageClass); currentLess2.AddCssClass(PageClassDisabled);
                    currentLess2.InnerHtml.Append("-");
                }

                currentLess1.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage - 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                currentLess1.AddCssClass(PageClass); currentLess1.AddCssClass(PageClassEnabled);
                currentLess1.InnerHtml.Append((currentPage - 1).ToString());
            }
            else // Disabled buttons if current page is 1
            {
                first.Attributes["href"] = uh.Action(PageAction, new { pageNum = 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                first.AddCssClass(PageClass); first.AddCssClass(PageClassDisabled);
                first.InnerHtml.Append("First");

                prev.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage - 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                prev.AddCssClass(PageClass); prev.AddCssClass(PageClassDisabled);
                prev.InnerHtml.Append("Prev");

                currentLess2.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage - 2, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                currentLess2.AddCssClass(PageClass); currentLess2.AddCssClass(PageClassDisabled);
                currentLess2.InnerHtml.Append("-");

                currentLess1.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage - 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                currentLess1.AddCssClass(PageClass); currentLess1.AddCssClass(PageClassDisabled);
                currentLess1.InnerHtml.Append("-");
            }

            current.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
            current.AddCssClass(PageClass); current.AddCssClass(PageClassSelected);
            current.InnerHtml.Append(currentPage.ToString());

            if (currentPage < PageModel.PageInfo.TotalPages)
            {
                currentPlus1.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage + 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                currentPlus1.AddCssClass(PageClass); currentPlus1.AddCssClass(PageClassEnabled);
                currentPlus1.InnerHtml.Append((currentPage + 1).ToString());

                if (currentPage < PageModel.PageInfo.TotalPages - 1)
                {
                    currentPlus2.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage + 2, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                    currentPlus2.AddCssClass(PageClass); currentPlus2.AddCssClass(PageClassEnabled);
                    currentPlus2.InnerHtml.Append((currentPage + 2).ToString());
                }
                else
                {
                    currentPlus2.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage + 2, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                    currentPlus2.AddCssClass(PageClass); currentPlus2.AddCssClass(PageClassDisabled);
                    currentPlus2.InnerHtml.Append("-");
                }

                next.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage + 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                next.AddCssClass(PageClass); next.AddCssClass(PageClassEnabled);
                next.InnerHtml.Append("Next");

                last.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.PageInfo.TotalPages, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                last.AddCssClass(PageClass); last.AddCssClass(PageClassEnabled);
                last.InnerHtml.Append("Last");
            }
            else
            {
                currentPlus1.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage + 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                currentPlus1.AddCssClass(PageClass); currentPlus1.AddCssClass(PageClassDisabled);
                currentPlus1.InnerHtml.Append("-");

                currentPlus2.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage + 2, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                currentPlus2.AddCssClass(PageClass); currentPlus2.AddCssClass(PageClassDisabled);
                currentPlus2.InnerHtml.Append("-");

                next.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPage + 1, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                next.AddCssClass(PageClass); next.AddCssClass(PageClassDisabled);
                next.InnerHtml.Append("Next");

                last.Attributes["href"] = uh.Action(PageAction, new { pageNum = PageModel.PageInfo.TotalPages, sex = Sex, hairColor = HairColor, ageAtDeath = AgeAtDeath, headDirection = HeadDirection, depth = Depth });
                last.AddCssClass(PageClass); last.AddCssClass(PageClassDisabled);
                last.InnerHtml.Append("Last");
            }

            TagBuilder count = new TagBuilder("a");
            count.AddCssClass(PageClass); count.AddCssClass(PageClassDisabled);
            count.InnerHtml.Append(PageModel.PageInfo.TotalPages.ToString() + " pages - " + PageModel.PageInfo.TotalNumBurials.ToString() + " results");


            final.InnerHtml.AppendHtml(first);
            final.InnerHtml.AppendHtml(prev);
            final.InnerHtml.AppendHtml(currentLess2);
            final.InnerHtml.AppendHtml(currentLess1);
            final.InnerHtml.AppendHtml(current);
            final.InnerHtml.AppendHtml(currentPlus1);
            final.InnerHtml.AppendHtml(currentPlus2);
            final.InnerHtml.AppendHtml(next);
            final.InnerHtml.AppendHtml(last);
            final.InnerHtml.AppendHtml(count);

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}

