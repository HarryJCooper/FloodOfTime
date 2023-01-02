#import "iOSPlugin.h"

extern UIViewController *UnityGetGLViewController();

@interface iOSPlugin ()
@property (weak, nonatomic) IBOutlet UITextView *receiveMotionDnaTextField;
@property (class, readonly, strong, nonatomic) MotionDnaSDK *motionDnaSDK;
@end

@implementation iOSPlugin

+(void)alertView:(NSString *)title addMessage:(NSString *)message
{
    UIAlertController *alert = [UIAlertController alertControllerWithTitle:title
                                                                   message:message
                                                            preferredStyle:UIAlertControllerStyleAlert];
    UIAlertAction *defaultAction = [UIAlertAction actionWithTitle:@"OK"
                                                            style:UIAlertActionStyleDefault
                                                          handler:^(UIAlertAction *action){}];
    [alert addAction:defaultAction];
    [UnityGetGLViewController() presentViewController:alert animated:YES completion:nil];
}

+ (MotionDnaSDK *)motionDnaSDK {
    static MotionDnaSDK *motionDnaSDK = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        motionDnaSDK = [[MotionDnaSDK alloc] initWithDelegate:(id<MotionDnaSDKDelegate>)self];
    });
    return motionDnaSDK;
}

+ (void)startDemo {
    NSLog(@"SDK Version: %@", MotionDnaSDK.SDKVersion);
    //    This functions starts up the SDK. You must pass in a valid developer's key in order for
    //    the SDK to function. IF the key has expired or there are other errors, you may receive
    //    those errors through the reportError() callback route.
    [self.motionDnaSDK startWithDeveloperKey:@"F47BHvGJVBm1eNuQVi0mgf3vSaljVEMG147yIMQznhQJcb8YU2J7nwXnYpv7vEca"];
    NSLog(@"Started demo");
}

+ (void)stopDemo {
    [self.motionDnaSDK stop];
    NSLog(@"Stopped demo");
}


static Location                               *MotionDnaLocation;
static Attitude                               *MotionDnaAttitude;
static NSString                               *MotionDnaID;
static NSDictionary<NSString *, Classifier *> *MotionDnaClassifiers;
static double                                  MotionDnaTimestamp;

+ (void)receiveMotionDna:(MotionDna *)motionDna {
    MotionDnaLocation    = motionDna.location;
    MotionDnaAttitude    = motionDna.attitude;
    MotionDnaID          = motionDna.ID;
    MotionDnaClassifiers = motionDna.classifiers;
    MotionDnaTimestamp   = motionDna.timestamp;
}

+(void)reportStatus:(MotionDnaSDKStatus)status message:(NSString *)message {
    switch (status) {
        case MotionDnaSDKStatusSensorTimingIssue:
            NSLog(@"Status: Sensor Timing %@", message);
            break;
        case MotionDnaSDKStatusAuthenticationFailure:
            NSLog(@"Status: Authentication Failed %@", message);
            break;
        case MotionDnaSDKStatusMissingSensor:
            NSLog(@"Status: Sensor Missing %@", message);
            break;
        case MotionDnaSDKStatusExpiredSDK:
            NSLog(@"Status: SDK Expired %@", message);
            break;
        case MotionDnaSDKStatusAuthenticationSuccess:
            NSLog(@"Status: Authentication Succeeded %@", message);
            break;
        case MotionDnaSDKStatusConfiguration:
            NSLog(@"Status: Configuration %@", message);
            break;
        default:
            NSLog(@"Status: Unknown %@", message);
    }
}
@end

extern "C"
{

    void _ShowAlert(const char *title, const char *message)
    {
        [iOSPlugin alertView:[NSString stringWithUTF8String:title] addMessage:[NSString stringWithUTF8String:message]];
    }// this matches the method we're importing from Unity

    void _start()
    {
        [iOSPlugin startDemo];
        NSLog(@"extern C _start");
    }// this matches the method we're importing from Unity

    char *cStringCopy(const char *string)
    {
        if (string == NULL)
            return NULL;

        char *res = (char *)malloc(strlen(string) + 1);
        strcpy(res, string);

        return res;
    }

    struct CartesianLocationStruct {
        double x;
        double y;
        double z;
        double heading;
    };

    struct GlobalLocationStruct {
        double latitude;
        double longitude;
        double altitude;
        double heading;
        int    accuracy; //GlobalLocationAccuracyLOW = 0, GlobalLocationAccuracyHIGH = 1
    };

    struct LocationStruct {
        struct CartesianLocationStruct cartesianLocation;
        struct GlobalLocationStruct    globalLocation;
    };

    struct LocationStruct _location(void) {
        struct CartesianLocationStruct cartesianLocation = (struct CartesianLocationStruct){
            .x = MotionDnaLocation.cartesian.x,
            .y = MotionDnaLocation.cartesian.y,
            .z = MotionDnaLocation.cartesian.z,
            .heading = MotionDnaLocation.cartesian.heading,
        };
        struct GlobalLocationStruct globalLocation = (struct GlobalLocationStruct){
            .latitude  = MotionDnaLocation.global.latitude,
            .longitude = MotionDnaLocation.global.longitude,
            .altitude  = MotionDnaLocation.global.altitude,
            .heading   = MotionDnaLocation.global.heading,
            .accuracy  = (MotionDnaLocation.global.accuracy == GlobalLocationAccuracyLOW) ? 0 : 1,
                        //GlobalLocationAccuracyLOW = 0, GlobalLocationAccuracyHIGH = 1
        };
        
        struct LocationStruct location = (struct LocationStruct){
            .cartesianLocation = cartesianLocation,
            .globalLocation = globalLocation
        };
        return location;
    }

    struct EulerStruct {
        double roll;
        double pitch;
        double yaw;
    };

    struct QuaternionStruct {
        double w;
        double x;
        double y;
        double z;
    };

    struct AttitudeStruct {
        struct EulerStruct euler;
        struct QuaternionStruct quaternion;
    };

    struct AttitudeStruct _attitude(void) {
        struct EulerStruct euler = (struct EulerStruct){
            .roll  = MotionDnaAttitude.euler.roll,
            .pitch = MotionDnaAttitude.euler.pitch,
            .yaw   = MotionDnaAttitude.euler.yaw,
        };
        
        struct QuaternionStruct quaternion = (struct QuaternionStruct){
            .w = MotionDnaAttitude.quaternion.w,
            .x = MotionDnaAttitude.quaternion.x,
            .y = MotionDnaAttitude.quaternion.y,
            .z = MotionDnaAttitude.quaternion.z,
        };
        
        struct AttitudeStruct attitude = (struct AttitudeStruct){
            .euler = euler,
            .quaternion = quaternion,
        };
        
        return attitude;
    }

    char *_ID(void) {
        return cStringCopy(MotionDnaID.UTF8String);
    }

    //NSDictionary<NSString *, Classifier *> *_classifiers() {
    //
    //}

    double _timestamp(void) {
        return MotionDnaTimestamp;
    }

}
