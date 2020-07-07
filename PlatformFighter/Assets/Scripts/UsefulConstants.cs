/* This class stores common constants that are used in multiple scripts
 * ex) animation states
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public enum AnimeState { IDLE, InAir, DownAir, UpAir, FAir, BackAir, NAir, DownTilt, UpTilt, Jab, FTilt, UpB, NeutralB, SideB, DownB, ReleaseB}; // add new states as necessary

    public const float MAXFALLSPEED = -12f;
    public const float MAXFASTFALLSPEED = -18F;
}
