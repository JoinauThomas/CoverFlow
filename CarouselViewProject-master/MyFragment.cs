
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
using Xamarin.NineOldAndroids.Views;
using System.IO;
using Android.Graphics;
using static Android.App.ActionBar;

namespace CarouselViewProjectmaster
{			
	public class MyFragment : Android.Support.V4.App.Fragment {

        private static int position = 0;
        public static int positionImg;
        public static void GetPosition(int position)
        {
            positionImg = position;

        }

        public MyFragment()
        {
            
		}

       
		public static Android.Support.V4.App.Fragment newInstance(MainActivity context, int pos, float scale,bool IsBlured)
		{
			Bundle b = new Bundle();
			b.PutInt("pos", pos);
			b.PutFloat("scale", scale);
			b.PutBoolean("IsBlured", IsBlured);	
			MyFragment myf = new MyFragment ();
            
			return Android.Support.V4.App.Fragment.Instantiate (context,myf.Class.Name, b);
		}

		public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
            
            int poss = positionImg;
            if (container == null) {
				return null;
			}

			LinearLayout l = (LinearLayout)inflater.Inflate(Resource.Layout.mf, container, false);


            //string p = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "photo1.jpg");

            //Bitmap bitmap = BitmapFactory.DecodeFile(p);
            //ResizeImage("p");

            
            int pos = this.Arguments.GetInt("pos");

			LinearLayout root = (LinearLayout) l.FindViewById(Resource.Id.root);
            ImageView img = (ImageView)l.FindViewById(Resource.Id.content);

            // seul la premiere image a une taille normale. les autres sont à l'échele 0.7
            if(pos != MainActivity.FIRST_PAGE)
            {
                ViewHelper.SetScaleX(root, MainActivity.SMALL_SCALE);
                ViewHelper.SetScaleY(root, MainActivity.SMALL_SCALE);
            }
            else
            {
                ViewHelper.SetScaleX(root, MainActivity.BIG_SCALE);
                ViewHelper.SetScaleY(root, MainActivity.BIG_SCALE);
            }
            
            img.Click += img_Click;


            int resourceId = (int)typeof(Resource.Drawable).GetField("photo"+pos).GetVa‌​lue(null);
            img.SetImageResource(resourceId);

            float scale = this.Arguments.GetFloat("scale");
			bool isBlured=this.Arguments.GetBoolean("IsBlured");
			if(isBlured)
			{
				ViewHelper.SetAlpha(root,MyPagerAdapter.getMinAlpha());
				ViewHelper.SetRotationY(root, MyPagerAdapter.getMinDegree());
			}
			return l;
		}

        

        // changer le format des images
        public string ResizeImage(string sourceFilePath)
        {
            Android.Graphics.Bitmap bmp = Android.Graphics.BitmapFactory.DecodeFile(sourceFilePath);

            string newPath = sourceFilePath.Replace(".jpg", ".png");
            using (var fs = new FileStream(newPath, FileMode.OpenOrCreate))
            {
                bmp.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, fs);
            }

            return newPath;
        }

        public void img_Click(object sender, EventArgs e)
        {
            // creation du dialog
            AlertDialog.Builder myDialog = new AlertDialog.Builder(Context);

            myDialog.SetTitle("Title");

            //recupération de l'image
            myDialog.SetView(GetImage());

            // création du bouton dans le dialog
            myDialog.SetPositiveButton("Select", (senderAlert, args) =>
            {
                ////////////////////////////////////////
                /////  mettre l'action voulue ici  /////
                ////////////////////////////////////////
                Toast.MakeText(Context, "image selected!", ToastLength.Short).Show();
            });

            // affichage du dialogue
            myDialog.Show();

        }
        public View GetImage()
        {
            ImageView img = new ImageView(Context);
            int resourceId = (int)typeof(Resource.Drawable).GetField("photo" + positionImg).GetVa‌​lue(null);
            img.SetImageResource(resourceId);
            img.SetAdjustViewBounds(true);

            return img;
        }

        }
}
