using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;
using LetsMeet.Models;
using LetsMeet.Data;
using Xamarin.Forms;

namespace LetsMeet.Controls
{
    class MeetingPin : Pin
    {
        public static readonly BindableProperty MeetingProperty = BindableProperty.Create(
            propertyName: "Meeting",
            returnType: typeof(Meeting),
            declaringType: typeof(MeetingPin),
            defaultValue: null);

        //private string _meetingId;
        public Meeting Meeting
        {
            get
            {
                return (Meeting)GetValue(MeetingProperty);
            }
            set
            {
                SetValue(MeetingProperty, value);
            }
        }
    }
}
