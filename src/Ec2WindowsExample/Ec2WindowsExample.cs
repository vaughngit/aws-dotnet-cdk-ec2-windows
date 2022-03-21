
using Constructs;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Logs;
using System;

namespace Ec2WindowsExample
{
    public class Ec2WindowsExample : Stack
    {
        internal Ec2WindowsExample(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {

            /*plaintext password generator intended for testing purposes only - */
            RandomPasswordGenerator randomPasswordGenerator = new RandomPasswordGenerator();   
            var user = "demouser";
            var pass = randomPasswordGenerator.GeneratePassword(true, true, true, true, 20);

            /*Import Default VPC */
            var vpc = Vpc.FromLookup(this, "import default vpc", new VpcLookupOptions
            {
                IsDefault = true
            });

            /* Create Instance SecurityGroup */
            var serverSG = new SecurityGroup(this, "create security group for instance", new SecurityGroupProps
            {
               Vpc = vpc,
                Description = "allow all egress traffic from instance",
                AllowAllOutbound = true

            });


            /* uncomment to allow port web traffic ingress into web server traffic */
            //  serverSG.AddIngressRule(Peer.AnyIpv4(), Port.Tcp(80), "allow web access from the world");

            /* uncomment to allow RDP Traffic ingress over the internet into server traffic */
            // serverSG.AddIngressRule(Peer.AnyIpv4(), Port.Tcp(3389), "allow RDP traffic over the network from anywhere");


            /*Create ec2 instance role with persmission to connect to System Manager and Post Logs to CloudWatch Logs */
            var role = new Role(this, "ec2 instance role", new RoleProps
            {
                AssumedBy = new ServicePrincipal("ec2.amazonaws.com"),
                RoleName = "CDK_Dotnet_EC2_Instance_Role",
                Description = "SSM IAM role",
                ManagedPolicies = new[] 
                { 
                    ManagedPolicy.FromAwsManagedPolicyName("AmazonSSMManagedInstanceCore"),
                   // ManagedPolicy.FromAwsManagedPolicyName("CloudWatchLogsFullAccess")
                }
  
            }) ;

            ///*  Add inline policy to role created above */
            role.AddToPolicy(new PolicyStatement(new PolicyStatementProps
            {

                Effect = Effect.ALLOW,
                Actions = new[] { "logs:DescribeLogGroups", "logs:CreateLogGroup" },
                Resources = new[] { "*" }

            }));
            role.AddToPolicy(new PolicyStatement(new PolicyStatementProps
            {

                Effect = Effect.ALLOW,
                Actions = new[] { "logs:CreateLogStream", "logs:DescribeLogStreams", "logs:PutLogEvents" },
                //Resources = new[] { "arn:aws:logs:*:*:log-group:/aws/systemManager/SessionManagerLogs" }, //specify write only to specific logGroup
                Resources = new[] { "*" },

            }));

            /*Configure UserData Scripts to create demouser and install base webserver */
            MultipartUserData multipartUserData = new MultipartUserData();
            UserData commandsUserData = UserData.ForWindows();
            multipartUserData.AddUserDataPart(commandsUserData, MultipartBody.SHELL_SCRIPT, true);
            commandsUserData.AddCommands(string.Format("$user=\"{0}\"", user));
            commandsUserData.AddCommands(string.Format("$secureString = ConvertTo-SecureString \"{0}\" -AsPlainText -Force", pass));
            commandsUserData.AddCommands("new-localuser $user -password $secureString");
            commandsUserData.AddCommands("Add-LocalGroupMember -Group \"Administrators\" -Member $user");
            commandsUserData.AddCommands("Get-LocalUser $user | select *");
           /* uncomment to install IIS webserver */
           // commandsUserData.AddCommands("Install-WindowsFeature -name Web-Server -IncludeManagementTools");

            /*Create EC2 Instance: */
            var ec2Instance = new Instance_(this, "ec2-instance", new InstanceProps {
               Vpc = vpc,
               Role = role,
               SecurityGroup = serverSG,
               VpcSubnets = new SubnetSelection { SubnetType = SubnetType.PUBLIC },
               MachineImage = MachineImage.LatestWindows(WindowsVersion.WINDOWS_SERVER_2022_ENGLISH_FULL_BASE),
               InstanceType = InstanceType.Of(InstanceClass.BURSTABLE3_AMD, InstanceSize.MEDIUM),
               //InstanceName = "TestWindowInstance-dotnet-cdk",
               UserData = commandsUserData               
            });

            new CfnOutput(this, "UserName", new CfnOutputProps { Value = user });
            new CfnOutput(this, "Password", new CfnOutputProps { Value = pass });
            new CfnOutput(this, "InstanceId", new CfnOutputProps { Value = ec2Instance.InstanceId });

            Amazon.CDK.Tags.Of(ec2Instance).Add("Name", "TestWindowInstance-dotnet-cdk");
        }
        
    }

}


