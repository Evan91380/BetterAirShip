using HarmonyLib;
using UnityEngine;

namespace BetterAirShip.Patch {

    [HarmonyPatch(typeof(AirshipStatus), nameof(AirshipStatus.OnEnable))]
    class Repositioning {
        public static void Postfix(AirshipStatus __instance) {

            MapConsole AdminTable = Object.FindObjectOfType<MapConsole>();

            if (BetterAirShip.MoveAdmin.GetValue() != 0)
            {
                GameObject MapFloating = GameObject.Find("Cockpit/cockpit_mapfloating");

                if (BetterAirShip.MoveAdmin.GetValue() == 1)
                {
                    // Admin
                    AdminTable.transform.position = new Vector2(-17.269f, 1.375f);
                    AdminTable.transform.rotation = Quaternion.Euler(new Vector3(0.000f, 0.000f, 350.316f));
                    AdminTable.transform.localScale = new Vector3(1f, 1f, 1f);

                    // Maping Float
                    MapFloating.transform.position = new Vector2(-17.736f, 2.36f);
                    MapFloating.transform.rotation = Quaternion.Euler(new Vector3(0.000f, 0.000f, 350f));
                    MapFloating.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                if (BetterAirShip.MoveAdmin.GetValue() == 2)
                {
                    // New Admin
                    AdminTable.transform.position = new Vector3(5.078f, 3.4f, 1f);
                    AdminTable.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 76.1f));
                    AdminTable.transform.localScale = new Vector3(1.200f, 1.700f, 1f);
                    MapFloating.transform.localScale = new Vector3(0f, 0f, 0f);
                }
            }

            if (BetterAirShip.MoveElectrical.GetValue() != 0)
            {
                GameObject Electrical = GameObject.Find("GapRoom/task_lightssabotage (gap)");

                if (BetterAirShip.MoveElectrical.GetValue() == 1)
                {
                    // Electical Cargo
                    Electrical.transform.position = new Vector2(-8.818f, 13.184f);
                    Electrical.transform.localScale = new Vector3(0.909f, 0.818f, 1f);

                    // Support
                    GameObject OriginalSupport = GameObject.Find("Vault/cockpit_comms");
                    GameObject SupportElectrical = Object.Instantiate(OriginalSupport, OriginalSupport.transform);
                    SupportElectrical.transform.position = new Vector2(-8.792f, 13.242f);
                    SupportElectrical.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                if (BetterAirShip.MoveElectrical.GetValue() == 2)
                {
                    // Light
                    Electrical.transform.position = new Vector2(19.339f, -3.665f);
                }
            }


            if (BetterAirShip.VitalsMedbay.GetValue())
            {
                // Vitals
                GameObject Vitals = GameObject.Find("Medbay/panel_vitals");
                Vitals.transform.position = new Vector2(24.55f, -4.780f);

                // Download Medbay
                GameObject MedbayDownload = GameObject.Find("Medbay/panel_data");
                MedbayDownload.transform.position = new Vector2(25.240f, -7.938f);
            }

            if (BetterAirShip.CargoGas.GetValue())
            {
                // Cargo gas
                GameObject Fuel = GameObject.Find("Storage/task_gas");
                Fuel.transform.position = new Vector2(36.070f, 1.897f);
            }

            if (BetterAirShip.Divert.GetValue())
            {
                // Divert
                GameObject DivertRecieve = GameObject.Find("HallwayMain/DivertRecieve");
                DivertRecieve.transform.position = new Vector2(13.35f, -1.659f);
            }

        }
    }
}
