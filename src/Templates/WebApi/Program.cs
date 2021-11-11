using Serilog;
using Serilog.Events;
using Xerris.WebApi1;

// ReSharper disable StringLiteralTypo
const string xerrisLogo = @"
              ^EDWXXXX&HT    >7777^     -7777/    ;}z]]ev>.     -777v. _?e+ |777=  |ce- 7777_    ,+le]]z=,   
              PH+     1HT    .LDDD&l   |XDDDx  _LXDDDDDDDR&Ui   iRDDRzPDDRn SDDDNxXDDR> RDDDs  |UDRDDDDDRDP+ 
 cEONNNNNWW   GH-     xH4      =WRDRF,l&DRX>  c&DDDV7++}nWDDDF. iRDDDDDGgUl SDDDRR&PgU~ RDDDs ;DDDDe|^>e&RRDt
rW$.     NW   GHWXXXX&&b.       _ERDDWDDD6,  +DDDD}      -mDDDL iRDDRF_     SDDDDz.     RDDDs |DDDDn=-  ^+++/
zW#     ,OX .-|||||||,            eDDDDDv    LRDDDDDDDDDDDDDDRO iRDDD/      SDDDN       RDDDs  +gDDDDRDmgns. 
zWOggggEmEi=SLeeeeeL$+           >NRDDDDP/   oDDD&ezzzzzzzzzzz} iDDDR-      SDDDP       RDDDs    .|7abEWDRDX+
.,,,,,,..  dC.     z$+          oDDDNSDRR&e  ,NDDDU_    ,nhhhd_ iDDDR-      SDDDP       RDDDs lpppd,    9DDRg
           dT.....:ud|        ,UDDDh. =XDDRp, ,dDRDDNphP&DDDG>  iDDDR-      SDDDP       RDDDs ,gDDRWpTCgDDR&}
           uuuuuuuxz;        >PmmmI    ,FmmmP^  _e$G&DDDXEn+    +mmmm,      #mmmF       mmmmi   |#Um&DD&Ohe- 
";
// ReSharper restore StringLiteralTypo

var builder = Host.CreateDefaultBuilder(args)
    .UseSerilog()
     .ConfigureWebHostDefaults(webBuilder =>
     {
         webBuilder.UseStartup<Startup>();
     });

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information(xerrisLogo);
    Log.Information("Starting web host");
    builder.Build().Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}
