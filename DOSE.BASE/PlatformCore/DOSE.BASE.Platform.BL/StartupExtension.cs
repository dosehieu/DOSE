using DOSE.BASE.PlatformCore.BL.Base;
using DOSE.BASE.PlatformCore.BL.Model;
using DOSE.BASE.PlatformCore.Common;
using DOSE.BASE.PlatformCore.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOSE.BASE.PlatformCore.BL
{
    public static class StartupExtension
    {
        public static void UserCoreBL(this IServiceCollection services, IConfiguration configuration)
        {
            //var licenseCell = new Aspose.Cells.License();
            //licenseCell.SetLicense("Aspose.Cells.lic");
            //var licenseWords = new Aspose.Words.License();
            //licenseWords.SetLicense("Aspose.Words.lic");
            //var licensePdf = new Aspose.Pdf.License();
            //licensePdf.SetLicense("Aspose.Pdf.lic");

            services.UseCoreService(configuration);

            services.UseDatabaseService();

            services.AddTransient<CoreServiceCollection, CoreServiceCollection>();
            services.AddTransient<BaseBL, BaseBL>();

        }
    }
}
