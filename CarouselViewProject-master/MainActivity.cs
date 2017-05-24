using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Java.Lang;

namespace CoverFlow
{
	[Activity (Label = "CoverFlow", MainLauncher = true)]
	public class MainActivity : FragmentActivity
	{
		public static int LOOPS = 10; 
		public static int FIRST_PAGE = 0;
		public static float BIG_SCALE = 1.0f;
		public static float SMALL_SCALE = 0.7f;
		public static float DIFF_SCALE = BIG_SCALE - SMALL_SCALE;
        public static TextView myTxt;

        public MyPagerAdapter adapter;
		public ViewPager pager;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            SetContentView (Resource.Layout.Main);
            
			pager = FindViewById<ViewPager>(Resource.Id.myviewpager);
			adapter = new MyPagerAdapter(this, this.SupportFragmentManager);
			pager.Adapter = adapter;
			pager.SetOnPageChangeListener (adapter);

			pager.SetCurrentItem (FIRST_PAGE,true);

            // nombre d'images chargées hors de l'écran
			pager.OffscreenPageLimit = 5;

            // recupération de l'orientation du téléphone 
            var surfaceOrientation = WindowManager.DefaultDisplay.Rotation;
            
            if (surfaceOrientation == SurfaceOrientation.Rotation0 || surfaceOrientation == SurfaceOrientation.Rotation180)
            {
                // règle l'espacement entre deux images et l'angle en mode portrait
                int i = (int)(Convert.ToInt32(GetString(Resource.String.pagermargin)));
                pager.PageMargin = Convert.ToInt32(GetString(Resource.String.pagermargin));
                float f = (float)Convert.ToDouble(GetString(Resource.String.anglePortrait));
                MyPagerAdapter.setminDegree(f);
            }
            else
            {
                // règle l'espacement entre deux images et l'angle en mode paysage
                int i = (int)(Convert.ToInt32(GetString(Resource.String.pagermargin)));
                pager.PageMargin = (int)(Convert.ToInt32(GetString(Resource.String.pagermargin))*2.5);
                float f = (float)Convert.ToDouble(GetString(Resource.String.angleLandscape));
                MyPagerAdapter.setminDegree(f);
            }
            //https://developer.xamarin.com/api/member/Android.Views.View.BringToFront()/
        }




    }
}


