using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.MapProviders;

namespace GIS_ex_210910 {
    public partial class Form1 : Form {
        #region field
        //marker
        GMapOverlay clickMarkers;
        GMapMarker marker;
        //route를 위한 list
        private List<PointLatLng> _points;
        //line 작성
        GMapRoute linelayer;
        GMapOverlay lineOverlay;
        //RTK 폼에서 데이터를 받아오기위한 static변수
        static public string RTK_GGAdata;
        //currpoint        
        public GMapOverlay currMarkers = new GMapOverlay("currMarker");
        public GMapMarker currMarker;
        private List<PointLatLng> _currPoints;
        public GMapRoute currRoute;
        public GMapOverlay currRouteOverlay;
        
        #endregion

        public Form1() {
            InitializeComponent();
            _points = new List<PointLatLng>();
            _currPoints = new List<PointLatLng>();
            this.btn_line.Click += btn_line_Click;
            this.btn_clear.Click += btn_clear_Click;            
            }

        private void Form1_Load(object sender, EventArgs e) {
            //폼 로드시 지도 표기            
            gmap.MapProvider = GMapProviders.GoogleSatelliteMap;
            GMap.NET.GMaps.Instance.Mode = AccessMode.ServerOnly;
            
            gmap.Position = new PointLatLng(36.399655912013735, 127.39978844890516);
                
            gmap.ShowCenter = true;            
            ////마커추가
            //GMapOverlay markers = new GMapOverlay("markers");
            //GMapMarker marker = new GMarkerGoogle(
            //    new PointLatLng(37.55363, 126.98837),
            //    GMarkerGoogleType.blue_dot
            //    );
            //markers.Markers.Add(marker);
            //gmap.Overlays.Add(markers);

            ////마커 툴팁
            //marker.ToolTipText = "hello\nout there";
            //marker.ToolTip.Fill = Brushes.White;
            //marker.ToolTip.Foreground = Brushes.Black;
            //marker.ToolTip.Stroke = Pens.Black;
            //marker.ToolTip.TextPadding = new Size(20, 20);
            //marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            
            ////add polygon
            //GMapOverlay polygons = new GMapOverlay("polygons");
            //List<PointLatLng> points = new List<PointLatLng>();
            //points.Add(new PointLatLng(37.555187, 126.981159));
            //points.Add(new PointLatLng(37.549465, 126.982747));
            //points.Add(new PointLatLng(37.545230, 126.989820));
            //points.Add(new PointLatLng(37.554386, 126.998337));
            //GMapPolygon polygon = new GMapPolygon(points, "seoul");
            //polygons.Polygons.Add(polygon);

            //polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            //polygon.Stroke = new Pen(Color.Red);
            //polygon.IsHitTestVisible = true;

            //gmap.Overlays.Add(polygons);
                        
            }

        private void gmap_OnMarkerClick(GMapMarker item, MouseEventArgs e) {
            //event properties에 있다
            MessageBox.Show(String.Format("Marker '{0}' was clicked.", item.Tag));
            //onMarkerEnter(cursor hovering over a marker) or OnmarkerLeave
            }

        private void gmap_OnPolygonClick(GMapPolygon item, MouseEventArgs e) {
            MessageBox.Show(String.Format("Polygon '{0}' whit tag{1} was clicked.", item.Name, item.Tag));
            }

        private void gmap_MouseClick(object sender, MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {

                var point = gmap.FromLocalToLatLng(e.X, e.Y);
                double lat = point.Lat;
                double lng = point.Lng;

                _points.Add(new PointLatLng(lat, lng));

                //이거하나추가로 멀티마커가 가능하네...아니다 이건 클릭할때마다 마커오버레이를 새로작성해서 가능한거임
                //LoadMap(point);
                //add marker
                addMarker(point, GMarkerGoogleType.green,_points.IndexOf(point));

                //lb_markerIndex.Text = clickMarkers.Markers[0].Position.ToString();
                //마커를 추가할때마다 새로운 clickMarker 객체가 생성되는데 이걸 수정해봐야...
                //오버레이가 내 생각과 조금 다른거같다...

                //label
                textIcon(_points.IndexOf(point));
                }            
            }

        private void LoadMap(PointLatLng point) {
            gmap.Position = point;

            }

        private void addMarker(PointLatLng pointToAdd, GMarkerGoogleType markerType, int markerNum) {
            clickMarkers = new GMapOverlay("clickMarker");
            marker = new GMarkerGoogle(pointToAdd,markerType);

            marker.Tag = "clickMarker" + markerNum;
            string location = "\r\n"
                               + "ClickMarker" + markerNum + "\r\n"
                               + pointToAdd.Lat.ToString() + "\r\n"
                               + pointToAdd.Lng.ToString();
            marker.ToolTipText = location;
            marker.ToolTip.Fill = Brushes.White;
            marker.ToolTip.Foreground = Brushes.Black;
            marker.ToolTip.Stroke = Pens.Black;
            marker.ToolTip.TextPadding = new Size(20, 20);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;

            gmap.Overlays.Add(clickMarkers);
            clickMarkers.Markers.Add(marker);
            }

        private void getRoute() {
            //route는 내가 원하는 기능이 아닌거 같아서
            //그림을 그리기로함...
            var route = GoogleMapProvider.Instance.GetRoute(_points[0], _points[1], false, false, 14);
            GMapRoute mapRoute = new GMapRoute(route.Points, "mapRoute");

            GMapOverlay routes = new GMapOverlay("routes");
            routes.Routes.Add(mapRoute);
            gmap.Overlays.Add(routes);
            }
        private void btn_line_Click(object sender, EventArgs e) {
        
            lineOverlay=new GMapOverlay("lineOverlay");
            linelayer = new GMapRoute("routeLine");
            linelayer.Stroke = new Pen(Brushes.Green, 2);

            lineOverlay.Routes.Add(linelayer);
            gmap.Overlays.Add(lineOverlay);

            for(int i = 0 ; i < _points.Count ; i++) {
                linelayer.Points.Add(_points[i]);
                }
            gmap.UpdateRouteLocalPosition(linelayer);
            }

        private void btn_clear_Click(object sender, EventArgs e) {

            }

        #region 현재위치표시
        public void addRtkMarker(string gga) {
            string[ ] splitGGA = gga.Split(',');
            string latDD = splitGGA[2].Substring(0, 2);
            string latMM = splitGGA[2].Substring(2);
            string lngDD = splitGGA[4].Substring(0, 3);
            string lngMM = splitGGA[4].Substring(3);

            double lat = Convert.ToDouble(latDD) + (Convert.ToDouble(latMM) / 60);
            double lng = Convert.ToDouble(lngDD) + (Convert.ToDouble(lngMM) / 60);

            if(!String.IsNullOrEmpty(latDD) && !String.IsNullOrEmpty(lngDD)){
                PointLatLng currPoint = new PointLatLng(lat,lng);                
                _currPoints.Add(currPoint);
                //현재위치로 이동
                gmap.Position = currPoint;                

                //marker
                
                //경로
                currRouteOverlay = new GMapOverlay("currRouteOverlay");
                currRoute = new GMapRoute("currRoute");
                currRoute.Stroke = new Pen(Brushes.Red, 2);

                for(int i = 0 ; i < _currPoints.Count ; i++) {
                    currRoute.Points.Add(_currPoints[i]);
                    if(i > 0) {                        
                        currMarkers.Clear();                       
                        }                                    
                    currMarker = new GMarkerGoogle(_currPoints[i], GMarkerGoogleType.red);
                    gmap.Overlays.Add(currMarkers);
                    currMarkers.Markers.Add(currMarker);
                    }

                gmap.Overlays.Add(currRouteOverlay);                
                currRouteOverlay.Routes.Add(currRoute);

                gmap.UpdateRouteLocalPosition(currRoute);                
                
                }
            }
        #endregion
        //overlay
        public void overlays(GMapOverlay overlay) {
            gmap.Overlays.Add(overlay);
            }
        #region label
        private GMarkerCross textIcon(int index ) {
            GMapOverlay markerLabel = new GMapOverlay("markerLabel");
            GMarkerCross crtMarker = new GMarkerCross(_points[index]);

            //transparent로 안보이게
            crtMarker.Pen = new Pen(Color.Transparent, 10);
            crtMarker.ToolTipText = (index+1).ToString();
            crtMarker.IsVisible = true;
            crtMarker.ToolTipMode = MarkerTooltipMode.Always;
            //툴팁이 가깝게...
            crtMarker.ToolTip.Offset = new Point(-17, -11);
            crtMarker.ToolTip.Fill = new SolidBrush(Color.Transparent);
            crtMarker.ToolTip.Stroke = new Pen(Color.Transparent);
            crtMarker.ToolTip.Font = new Font(FontFamily.GenericMonospace, 13,FontStyle.Bold);
            crtMarker.ToolTip.Foreground = new SolidBrush(Color.Red);

            gmap.Overlays.Add(markerLabel);
            markerLabel.Markers.Add(crtMarker);

            return crtMarker;
            }
        #endregion
        }//Form end
    }
