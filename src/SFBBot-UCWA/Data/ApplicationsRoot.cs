using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFBBot_UCWA.Data
{
    public class ApplicationsRoot
    {
        public string culture { get; set; }
        public string userAgent { get; set; }
        public _Links _links { get; set; }
        public _Embedded _embedded { get; set; }
        public string rel { get; set; }
    }

    public class _Links
    {
        public Self self { get; set; }
        public Reportmynetwork reportMyNetwork { get; set; }
        public Policies policies { get; set; }
        public Batch batch { get; set; }
        public Events events { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

    public class Reportmynetwork
    {
        public string href { get; set; }
    }

    public class Policies
    {
        public string href { get; set; }
    }

    public class Batch
    {
        public string href { get; set; }
    }

    public class Events
    {
        public string href { get; set; }
    }

    public class _Embedded
    {
        public Me me { get; set; }
        public People people { get; set; }
        public Onlinemeetings onlineMeetings { get; set; }
        public Communication communication { get; set; }
    }

    public class Me
    {
        public string name { get; set; }
        public string uri { get; set; }
        public string[] emailAddresses { get; set; }
        public string title { get; set; }
        public string department { get; set; }
        public _Links1 _links { get; set; }
        public string rel { get; set; }
    }

    public class _Links1
    {
        public Self1 self { get; set; }
        public Makemeavailable makeMeAvailable { get; set; }
        public Callforwardingsettings callForwardingSettings { get; set; }
        public Phones phones { get; set; }
        public Photo photo { get; set; }
    }

    public class Self1
    {
        public string href { get; set; }
    }

    public class Makemeavailable
    {
        public string href { get; set; }
    }

    public class Callforwardingsettings
    {
        public string href { get; set; }
    }

    public class Phones
    {
        public string href { get; set; }
    }

    public class Photo
    {
        public string href { get; set; }
    }

    public class People
    {
        public _Links2 _links { get; set; }
        public string rel { get; set; }
    }

    public class _Links2
    {
        public Self2 self { get; set; }
        public Presencesubscriptions presenceSubscriptions { get; set; }
        public Subscribedcontacts subscribedContacts { get; set; }
        public Presencesubscriptionmemberships presenceSubscriptionMemberships { get; set; }
        public Mygroups myGroups { get; set; }
        public Mygroupmemberships myGroupMemberships { get; set; }
        public Mycontacts myContacts { get; set; }
        public Myprivacyrelationships myPrivacyRelationships { get; set; }
        public Mycontactsandgroupssubscription myContactsAndGroupsSubscription { get; set; }
        public Search search { get; set; }
    }

    public class Self2
    {
        public string href { get; set; }
    }

    public class Presencesubscriptions
    {
        public string href { get; set; }
    }

    public class Subscribedcontacts
    {
        public string href { get; set; }
    }

    public class Presencesubscriptionmemberships
    {
        public string href { get; set; }
    }

    public class Mygroups
    {
        public string href { get; set; }
    }

    public class Mygroupmemberships
    {
        public string href { get; set; }
    }

    public class Mycontacts
    {
        public string href { get; set; }
    }

    public class Myprivacyrelationships
    {
        public string href { get; set; }
    }

    public class Mycontactsandgroupssubscription
    {
        public string href { get; set; }
    }

    public class Search
    {
        public string href { get; set; }
    }

    public class Onlinemeetings
    {
        public _Links3 _links { get; set; }
        public string rel { get; set; }
    }

    public class _Links3
    {
        public Self3 self { get; set; }
        public Myonlinemeetings myOnlineMeetings { get; set; }
        public Onlinemeetingdefaultvalues onlineMeetingDefaultValues { get; set; }
        public Onlinemeetingeligiblevalues onlineMeetingEligibleValues { get; set; }
        public Onlinemeetinginvitationcustomization onlineMeetingInvitationCustomization { get; set; }
        public Onlinemeetingpolicies onlineMeetingPolicies { get; set; }
        public Phonedialininformation phoneDialInInformation { get; set; }
    }

    public class Self3
    {
        public string href { get; set; }
    }

    public class Myonlinemeetings
    {
        public string href { get; set; }
    }

    public class Onlinemeetingdefaultvalues
    {
        public string href { get; set; }
    }

    public class Onlinemeetingeligiblevalues
    {
        public string href { get; set; }
    }

    public class Onlinemeetinginvitationcustomization
    {
        public string href { get; set; }
    }

    public class Onlinemeetingpolicies
    {
        public string href { get; set; }
    }

    public class Phonedialininformation
    {
        public string href { get; set; }
    }

    public class Communication
    {
        public string _675826ea8fb54e6d84fe0ac74d50b369 { get; set; }
        public object[] supportedModalities { get; set; }
        public string[] supportedMessageFormats { get; set; }
        public bool publishEndpointLocation { get; set; }
        public _Links4 _links { get; set; }
        public string rel { get; set; }
        public string etag { get; set; }
    }

    public class _Links4
    {
        public Self4 self { get; set; }
        public Startphoneaudio startPhoneAudio { get; set; }
        public Conversations conversations { get; set; }
        public Startmessaging startMessaging { get; set; }
        public Startonlinemeeting startOnlineMeeting { get; set; }
        public Joinonlinemeeting joinOnlineMeeting { get; set; }
    }

    public class Self4
    {
        public string href { get; set; }
    }

    public class Startphoneaudio
    {
        public string href { get; set; }
    }

    public class Conversations
    {
        public string href { get; set; }
    }

    public class Startmessaging
    {
        public string href { get; set; }
    }

    public class Startonlinemeeting
    {
        public string href { get; set; }
    }

    public class Joinonlinemeeting
    {
        public string href { get; set; }
    }

}
