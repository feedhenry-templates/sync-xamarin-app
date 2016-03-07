/**
 * Copyright Red Hat, Inc, and individual contributors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using FHSDK.Sync;
using sync.model;
using UIKit;

namespace sync_xamarin_app
{
    public partial class DetailsViewController : UIViewController
    {
        public DetailsViewController(IntPtr handle) : base(handle)
        {
        }

        public ShoppingItem Item { get; set; }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var tap = new UITapGestureRecognizer(() => { nameField.ResignFirstResponder(); });

            View.AddGestureRecognizer(tap);

            if (Item != null)
            {
                nameField.Text = Item.Name;
                createdField.Text = Item.GetCreatedTime();
                createLabel.Hidden = false;
                createdField.Hidden = false;
            }
            else
            {
                createLabel.Hidden = true;
                createdField.Hidden = true;
                createdField.Text = "";
            }
        }

        partial void SaveItem(UIBarButtonItem sender)
        {
            var name = nameField.Text;
            if (string.IsNullOrEmpty(name))
            {
                var alert = new UIAlertView("Error", "Name can not be empty", null, "Dismiss", null);
                alert.DismissWithClickedButtonIndex(0, false);
                alert.Show();
                return;
            }
            var client = FHSyncClient.GetInstance();
            if (Item == null)
            {
                client.Create(RootViewController.DatasetId, new ShoppingItem(name));
            }
            else
            {
                Item.Name = name;
                client.Update(RootViewController.DatasetId, Item);
            }

            Navigate();
        }

        partial void Cancel(UIBarButtonItem sender)
        {
            Navigate();
        }

        private void Navigate()
        {
            ((UINavigationController) ParentViewController).PopViewController(true);
        }
    }
}