using System.Collections.Generic;
using PaperNestTest.Enums;

namespace PaperNestTest.Class
{
    /// <summary>
    /// Dictionary linking the network providers to their code.
    /// SOURCE : https://fr.wikipedia.org/wiki/Mobile_Network_Code#Tableau_des_MNC_pour_la_France_m%C3%A9tropolitaine
    /// </summary>
    public static class NetworkProvidersDictionary
    {
        public static Dictionary<string, NetworkProvider> dico = new Dictionary<string, NetworkProvider>(){
            {"20801", NetworkProvider.Orange},
            {"20802", NetworkProvider.Orange},
            {"20803", NetworkProvider.Mobiquithings},
            {"20804", NetworkProvider.Sisteer},
            {"20805", NetworkProvider.Globalstar},
            {"20806", NetworkProvider.Globalstar},
            {"20807", NetworkProvider.Globalstar},
            {"20808", NetworkProvider.SFR},
            {"20809", NetworkProvider.SFR},
            {"20810", NetworkProvider.SFR},
            {"20811", NetworkProvider.SFR},
            {"20812", NetworkProvider.HewlettPackard},
            {"20813", NetworkProvider.SFR},
            {"20814", NetworkProvider.RFF},
            {"20815", NetworkProvider.Free},
            {"20816", NetworkProvider.Free},
            {"20817", NetworkProvider.Legos},
            {"20818", NetworkProvider.Voxbone},
            {"20819", NetworkProvider.Altitude},
            {"20820", NetworkProvider.BouyguesTelecom},
            {"20821", NetworkProvider.BouyguesTelecom},
            {"20822", NetworkProvider.Transatel},
            {"20823", NetworkProvider.VirginMobile},
            {"20824", NetworkProvider.Mobiquithings},
            {"20825", NetworkProvider.Lycamobile},
            {"20826", NetworkProvider.NRJMobile},
            {"20827", NetworkProvider.Coriolis},
            {"20828", NetworkProvider.AIF},
            {"20829", NetworkProvider.IMC},
            {"20830", NetworkProvider.SymaMobile},
            {"20831", NetworkProvider.Vectone},
            {"20835", NetworkProvider.Free},
            {"20888", NetworkProvider.Bouygues},
            {"20889", NetworkProvider.FondationBCom},
            {"20890", NetworkProvider.AssociationImagesReseaux},
            {"20891", NetworkProvider.Orange},
            {"20892", NetworkProvider.AssociationPlateformeTelecom},
            {"20893", NetworkProvider.Thales},
            {"20894", NetworkProvider.Halys},
            {"20895", NetworkProvider.Orange},
            {"20896", NetworkProvider.Axione},
            {"20897", NetworkProvider.Thales},
            {"20898", NetworkProvider.AirFrance}
        };
    }
}