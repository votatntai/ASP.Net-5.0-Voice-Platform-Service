using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Utility.Enums
{
    public enum UserStatus
    {
        Activated,
        Banned
    }

    public enum ArtistStatus
    {
        Activated,
        Banned
    }

    public enum ProjectStatus
    {
        Waiting,
        Pending,
        Process,
        Done,
        Delete,
        Deny
    }

    public enum InviteStatus
    {
        InvitePending,
        ResponsePending,
        Accept,
        Deny,
        Done
    }

    public enum FileStatus
    {
        Pending,
        Accept,
        Deny
    }
}
