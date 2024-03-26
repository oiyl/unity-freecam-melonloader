using MelonLoader;
using UnityEngine;
using XInputDotNetPure;

namespace unity_freecam
{
    public class freecam : MelonMod
    {
        private bool use_controller = false;

        private Camera main_camera;
        private Camera cached_camera;
        private Camera our_camera;

        private bool using_freecam = false;

        private void cache_freecam ( )
        {
            main_camera = Camera.main;
            if ( main_camera != null )
                cached_camera = main_camera;
            else
                MelonLogger.Warning ( "no camera found" );
        }

        private void enable_freecam ( )
        {
            using_freecam = true;
            cache_freecam ( );

            if ( cached_camera != null )
            {
                cached_camera.enabled = false;

                if ( our_camera == null )
                {
                    our_camera = new GameObject ( "freecam" ).AddComponent<Camera> ( );
                    our_camera.gameObject.tag = "MainCamera";

                    /* camera should persist on new scenes */
                    GameObject.DontDestroyOnLoad ( our_camera.gameObject );

                    our_camera.gameObject.hideFlags = HideFlags.HideAndDontSave;
                }
                /* set pos and rot to cached camera */
                our_camera.transform.position = cached_camera.transform.position;
                our_camera.transform.rotation = cached_camera.transform.rotation;

                our_camera.gameObject.SetActive ( true );
                our_camera.enabled = true;
            }
            else
            {
                MelonLogger.Error ( "error: no camera" );
            }
        }

        private void disable_freecam ( )
        {
            using_freecam = false;
            if ( cached_camera != null )
            {
                cached_camera.enabled = true;
            }

            if ( our_camera != null )
            {
                UnityEngine.Object.Destroy ( our_camera.gameObject );
            }
        }

        public override void OnUpdate ( )
        {
            if ( Input.GetKeyDown ( KeyCode.F5 ) )
            {
                if ( !using_freecam )
                    enable_freecam ( );
                else
                    disable_freecam ( );
            }

            if ( using_freecam )
            {
                float speed = 0.1f;
                float rotation_speed = 1.0f;

                if ( use_controller )
                {
                    /* if you so wish to do so, PlayerIndex can be switched out for another controller
                     * to see all controllers connected, you can utilize the following code:
                    
                    for ( int i = 0; i < 4; i++ ) // note: XInput wrapper only supports 4 controllers 
                        {
                            GamePadState state = GamePad.GetState ( ( PlayerIndex ) i );
                            if ( state.IsConnected )
                                MelonLogger.Msg ( "controller " + i + " is connected" );
                        }
                     */
                    GamePadState controller_state = GamePad.GetState ( PlayerIndex.One );

                    if ( controller_state.IsConnected )
                    {
                        UnityEngine.Vector2 left_stick = new UnityEngine.Vector2 ( controller_state.ThumbSticks.Left.X , controller_state.ThumbSticks.Left.Y );
                        UnityEngine.Vector2 right_stick = new UnityEngine.Vector2 ( controller_state.ThumbSticks.Right.X , controller_state.ThumbSticks.Right.Y );

                        /* Lock z rotation */
                        if ( our_camera.transform.eulerAngles.z != 0 )
                            our_camera.transform.eulerAngles = new UnityEngine.Vector3 ( our_camera.transform.eulerAngles.x , our_camera.transform.eulerAngles.y , 0 );

                        UnityEngine.Vector3 move_rotation = new UnityEngine.Vector3 ( left_stick.x , 0 , left_stick.y ) * speed;
                        our_camera.transform.Translate ( move_rotation );

                        UnityEngine.Vector3 rotation_delta = new UnityEngine.Vector3 ( -right_stick.y , right_stick.x , 0 ) * rotation_speed;
                        our_camera.transform.Rotate ( rotation_delta );
                    }
                }
                else
                {
                    if ( Input.GetKey ( KeyCode.LeftArrow ) || Input.GetKey ( KeyCode.A ) )
                        our_camera.transform.position += our_camera.transform.right * -speed;

                    if ( Input.GetKey ( KeyCode.RightArrow ) || Input.GetKey ( KeyCode.D ) )
                        our_camera.transform.position += our_camera.transform.right * speed;

                    if ( Input.GetKey ( KeyCode.UpArrow ) || Input.GetKey ( KeyCode.W ) )
                        our_camera.transform.position += our_camera.transform.forward * speed;

                    if ( Input.GetKey ( KeyCode.DownArrow ) || Input.GetKey ( KeyCode.S ) )
                        our_camera.transform.position += our_camera.transform.forward * -speed;

                    if ( Input.GetKey ( KeyCode.Space ) || Input.GetKey ( KeyCode.PageUp ) )
                        our_camera.transform.position += our_camera.transform.up * speed;

                    if ( Input.GetKey ( KeyCode.LeftControl ) || Input.GetKey ( KeyCode.PageDown ) )
                        our_camera.transform.position += our_camera.transform.up * -speed;

                    /* mouse movement */
                    our_camera.transform.Rotate ( new UnityEngine.Vector3 ( -Input.GetAxis ( "Mouse Y" ) , Input.GetAxis ( "Mouse X" ) , 0 ) * rotation_speed );

                    /* lock z rotation */
                    if ( our_camera.transform.eulerAngles.z != 0 )
                        our_camera.transform.eulerAngles = new UnityEngine.Vector3 ( our_camera.transform.eulerAngles.x , our_camera.transform.eulerAngles.y , 0 );

                }
            }

        }
    }
}
