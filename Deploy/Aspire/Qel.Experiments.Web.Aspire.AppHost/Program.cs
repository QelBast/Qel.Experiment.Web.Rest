var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Qel_Experiments_Web_Rest_GatewayApi>("gateway")
    .WithHttpEndpoint(port: 51200, name: "gateway");
builder.AddProject<Projects.Qel_Experiments_Web_Rest_BlacklistApi>("blacklist")
    .WithHttpEndpoint(port: 52200, name: "blacklist");
builder.AddProject<Projects.Qel_Experiments_Web_Rest_PassportProviderApi>("passport")
    .WithHttpEndpoint(port: 53200, name: "passport");
builder.AddProject<Projects.Qel_Experiments_Web_Rest_RequestProvider>("request");

builder.Build().Run();
