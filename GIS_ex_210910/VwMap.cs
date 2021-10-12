using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GMap.NET.MapProviders;
using GMap;
using GMap.NET;
using System.Reflection;
using GMap.NET.Projections;
using System.Globalization;

namespace GIS_ex_210910 {

    public class VwMap :GMapProvider {

        public static readonly VwMap Instance;

        public static string CustomURL = "http://api.vworld.kr/req/wmts/1.0.0/{0}/Satellite/{1}/{2}/{3}.jpeg";
        //base
        //public static string CustomURL = "http://api.vworld.kr/req/wmts/1.0.0/{0}/Base/{1}/{2}/{3}.png
        public string apiKey = "D586A1EC-EA9C-3DF2-8186-902701509EE5";

        VwMap( ) {
            MaxZoom = 22;
            MinZoom = 2;
            }
        static VwMap( ) {
            Instance = new VwMap();

            Type myType = typeof(GMapProviders);
            FieldInfo field = myType.GetField("DbHash", BindingFlags.Static | BindingFlags.NonPublic);
            Dictionary<int, GMapProvider> list = (Dictionary<int, GMapProvider>) field.GetValue(Instance);

            list.Add(Instance.DbId, Instance);
            }

        #region GmapProvider Members
        readonly Guid id = new Guid("4574218D-B552-4CAF-89AE-F20951BBDB2B");
        public override Guid Id {
            get {
                return id;
                }
            }
        readonly string name = "VwMap";
        public override string Name {
            get {
                return name;
                }
            }

        public override PureProjection Projection {
            get {
                return MercatorProjection.Instance;
                }
            }

        GMapProvider[ ] overlays;
        public override GMapProvider[ ] Overlays {
            get {
                if(overlays == null) {
                    overlays = new GMapProvider[ ] { this };
                    }
                return overlays;
                }
            }
        #endregion
        public override PureImage GetTileImage(GPoint pos, int zoom) {
            string url = MakeTileImageUrl(pos, zoom, LanguageStr);
            return GetTileImageUsingHttp(url);
            }

        private string MakeTileImageUrl(GPoint pos, int zoom, string languageStr) {
            string ret;
            ret = string.Format(CultureInfo.InvariantCulture, CustomURL, apiKey, zoom, pos.Y, pos.X);
            return ret;
            }


        }
    }
