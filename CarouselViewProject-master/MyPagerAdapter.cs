
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Xamarin.NineOldAndroids.Views;

namespace CoverFlow
{
    
    public class MyPagerAdapter : FragmentPagerAdapter, ViewPager.IOnPageChangeListener {

        private float angle;
		private bool swipedLeft=false;
		private int lastPage=9;
		private LinearLayout cur = null;
		private LinearLayout next = null;
		private LinearLayout prev = null;
		//private MyLinearLayout prevprev = null;
		private LinearLayout nextnext = null;
        private LinearLayout prevprev = null;
        private LinearLayout nextnextnext = null;
        private LinearLayout prevprevprev = null;
        private MainActivity context;
		private Android.Support.V4.App.FragmentManager fm;
		private float scale;
		private bool IsBlured;
		private static float minAlpha=0.6f;
		private static float maxAlpha=1f;
        public static float minDegree = -60.0f;

        //private int counter = 0;

        

        public static float getMinDegree()
		{
			return minDegree;
		}
        public static void setminDegree( float degree)
        {
            minDegree = degree;
        }
		public static float getMinAlpha()
		{
			return minAlpha;
		}
		public static float getMaxAlpha()
		{
			return maxAlpha;
		}

		public MyPagerAdapter(MainActivity context, Android.Support.V4.App.FragmentManager fm) :base (fm) {
			this.fm = fm;
			this.context = context;


		}

		public override Android.Support.V4.App.Fragment GetItem (int position)
		{
			if (position == MainActivity.FIRST_PAGE)
				scale = MainActivity.BIG_SCALE;     	
			else
			{
				scale = MainActivity.SMALL_SCALE;
				IsBlured=true;

			}

			Console.WriteLine("position =========== "+ position.ToString());
			Android.Support.V4.App.Fragment curFragment= MyFragment.newInstance(context, position, scale,IsBlured);
	
            return curFragment;
		}


        // définit le nombre d'éléments dans la lste
		public override int Count {
			get {
				return 5;
			}
		}
			

		void ViewPager.IOnPageChangeListener.OnPageScrollStateChanged (int state)
		{
			//throw new NotImplementedException ();
		}


        // charge les nouvelles vues losqu'on fait tourner le carousel
		void ViewPager.IOnPageChangeListener.OnPageScrolled (int position, float positionOffset, int positionOffsetPixels)
		{

			if (positionOffset >= 0f && positionOffset <= 1f)
			{
				positionOffset=positionOffset*positionOffset;
				cur = getRootView(position);
				next = getRootView(position +1);
				prev = getRootView(position -1);
				nextnext=getRootView(position +2);
                prevprev = getRootView(position - 2);
                nextnextnext = getRootView(position + 3);
                prevprevprev = getRootView(position - 3);
                
                if (cur != null) {
					ViewHelper.SetAlpha (cur, maxAlpha - 0.5f * positionOffset);

                    float val = 1 - 0.3f * positionOffset;
                    if(val <0.7f)
                    {
                        val = 0.7f;
                    }
                    ViewHelper.SetScaleX(cur, val);
                    ViewHelper.SetScaleY(cur, val);
                   
                }
				if (next != null) {
					ViewHelper.SetAlpha (next, minAlpha + 20.0f * positionOffset);

                    
                    float y =  0.7f + 0.3f *positionOffset;
                    if (y > 1.0f)
                    {
                        y = 1.0f;
                    }
                    ViewHelper.SetScaleY(next, y);
                    ViewHelper.SetScaleX(next, y);
                }
				if (prev != null) {
					ViewHelper.SetAlpha (prev, minAlpha + 20.0f * positionOffset);
                }
                if(prevprev != null)
                {
                    ViewHelper.SetScaleX(prevprev, 0.7f);
                    ViewHelper.SetScaleY(prevprev, 0.7f);
                    
                }

				if(nextnext!=null)
				{
                    //ViewHelper.SetScaleY(nextnext, 0.7f);
                    ViewHelper.SetAlpha(nextnext, minAlpha);
					ViewHelper.SetRotationY(nextnext, -minDegree);

                    ViewHelper.SetScaleX(nextnext, 0.7f);
                    ViewHelper.SetScaleY(nextnext, 0.7f);
                    next.BringToFront();
                }
                if(prevprevprev != null)
                {
                    ViewHelper.SetScaleX(prevprevprev, 0.7f);
                    ViewHelper.SetScaleY(prevprevprev, 0.7f);
                }
                if(nextnextnext != null)
                {
                    ViewHelper.SetScaleX(nextnextnext, 0.7f);
                    ViewHelper.SetScaleY(nextnextnext, 0.7f);
                }
				if(cur!=null)
				{
					ViewHelper.SetRotationY(cur, 0);
				}

				if(next!=null)
				{
					ViewHelper.SetRotationY(next, -minDegree);
				}
				if(prev!=null)
				{
					ViewHelper.SetRotationY(prev, minDegree);
				}
                 

				/*To animate it properly we must understand swipe direction
			 * this code adjusts the rotation according to direction.
			 */
				if(swipedLeft)
				{
					if(next!=null)
						ViewHelper.SetRotationY(next, -minDegree+minDegree*positionOffset);
					if(cur!=null)
						ViewHelper.SetRotationY(cur, 0+minDegree*positionOffset);
				}
				else 
				{
					if(next!=null)
						ViewHelper.SetRotationY(next, -minDegree+minDegree*positionOffset);
					if(cur!=null)
					{
						ViewHelper.SetRotationY(cur, 0+minDegree*positionOffset);
					}
				}
			}
			if(positionOffset>=1f)
			{
				ViewHelper.SetAlpha(cur, maxAlpha);
			}
            
		}

        void ViewPager.IOnPageChangeListener.OnPageSelected(int position)
        {
            MyFragment.GetPosition(position);
            ImageView monImg = new ImageView(this.context);
            monImg.FindViewById<ImageView>(Resource.Id.content);




            //if(position>1)
            //{
            //    var p = prev.LayoutParameters;
            //    p.Width = 500;
            //    prev.LayoutParameters = p;
            //}
            
            //var q = cur.LayoutParameters;
            //var r = next.LayoutParameters;

           
            //q.Width = 700;
            //r.Width = 500;

            //cur.LayoutParameters = q;
            //next.LayoutParameters = r;

            //int y = next.Width;

            if (lastPage <= position)
            {
                swipedLeft = true;
            }
            else if (lastPage > position)
            {
                swipedLeft = false;
            }
            
            lastPage = position;
        }




		private LinearLayout getRootView(int position)
		{
            LinearLayout ly;
			try {
				ly = (LinearLayout) fm.FindFragmentByTag(this.getFragmentTag(position)).View.FindViewById(Resource.Id.root);
			} catch (Exception e) {
				return null;
			}
			if(ly!=null)
				return ly;
			return null;
		}

		private String getFragmentTag(int position)
		{

			return "android:switcher:" + context.pager.Id + ":" + position;

		}
        


    }
}