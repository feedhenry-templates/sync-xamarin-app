using System;
using UIKit;
using FHSDK.Sync;

namespace sync_xamarin_app
{
	public partial class DetailsViewController: UIViewController
	{
		public ShoppingItem Item { get; set; }

		public DetailsViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			UITapGestureRecognizer tap = new UITapGestureRecognizer (() => {
				nameField.ResignFirstResponder();
			});

			View.AddGestureRecognizer (tap);

			if (Item != null) {
				nameField.Text = Item.Name;
				createdField.Text = Item.GetCreatedTime ();
				createLabel.Hidden = false;
				createdField.Hidden = false;
			} else {
				createLabel.Hidden = true;
				createdField.Hidden = true;
				createdField.Text = "";
			}
		}

		partial void SaveItem (UIBarButtonItem sender)
		{
			var name = nameField.Text;
			if (string.IsNullOrEmpty(name)) {
				var alert = new UIAlertView("Error", "Name can not be empty", null, "Dismiss", null);
				alert.DismissWithClickedButtonIndex(0, false);
				alert.Show();
				return;
			}
			else {
				var client = FHSyncClient.GetInstance();
				if (Item == null) {
					client.Create(RootViewController.DatasetId, new ShoppingItem(name));
				} else {
					Item.Name = name;
					client.Update(RootViewController.DatasetId, Item);
				}
			}

			Navigate();
		}

		partial void Cancel (UIBarButtonItem sender)
		{
			Navigate();
		}

		private void Navigate() 
		{
			((UINavigationController)ParentViewController).PopViewController(true);
		}
	}
}