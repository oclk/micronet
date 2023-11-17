using AutoMapper;
using IdentityService.Application.Common.Interfaces.Clients;
using IdentityService.Application.Features.Groups.Commands.CreateGroup;
using IdentityService.Application.Features.Groups.Commands.DeleteGroup;
using IdentityService.Application.Features.Groups.Commands.SetOrCreateSubGroup;
using IdentityService.Application.Features.Groups.Commands.UpdateGroup;
using IdentityService.Application.Features.Groups.Commands.UpdateGroupManagementPermissions;
using IdentityService.Application.Features.Groups.Queries.GetGroup;
using IdentityService.Application.Features.Groups.Queries.GetGroupManagementPermissions;
using IdentityService.Application.Features.Groups.Queries.GetGroupMembers;
using IdentityService.Application.Features.Groups.Queries.GetGroups;
using IdentityService.Application.Features.Groups.Queries.GetGroupsCount;

namespace IdentityService.Application.Common.Profiles;

/// <summary>
/// AutoMapper profile that maps commands and queries related to group operations in the Identity Service application to their respective request types.
/// </summary>
public class GroupsMappingProfile : Profile
{
    /// <summary>
    /// Initializes the GroupsMappingProfile class and configures AutoMapper mappings for group operations.
    /// </summary>
	public GroupsMappingProfile()
	{
        // CreateGroup
        CreateMap<CreateGroupCommand, CreateGroupRequest>();
        CreateMap<GroupRepresentation, CreateGroupCommandDto>();

        // DeleteGroup
        CreateMap<DeleteGroupCommand, DeleteGroupRequest>();

        // GetGroup
        CreateMap<GetGroupQuery, GetGroupRequest>();
        CreateMap<GroupRepresentation, GetGroupQueryVm>();

        // GetGroupManagementPermissions
        CreateMap<GetGroupManagementPermissionsQuery, GetGroupManagementPermissionsRequest>();
        CreateMap<GroupManagementPermissions, GetGroupManagementPermissionsQueryVm>();

        // GetGroupMembers
        CreateMap<GetGroupMembersQuery, GetGroupMembersRequest>();
        CreateMap<GetGroupMembersResponse, GetGroupMembersQueryVm>();

        // GetGroups
        CreateMap<GetGroupsQuery, GetGroupsRequest>();
        CreateMap<GetGroupsResponse, GetGroupsQueryVm>();

        // GetGroupsCount
        CreateMap<GetGroupsCountQuery, GetGroupsCountRequest>();
        CreateMap<GetGroupsCountResponse, GetGroupsCountQueryVm>();

        // SetOrCreateSubGroup
        CreateMap<SetOrCreateSubGroupCommand, SetOrCreateSubGroupRequest>();
        CreateMap<GroupRepresentation, SetOrCreateSubGroupCommandGroupRepresentation>();

        // UpdateGroup
        CreateMap<UpdateGroupCommand, UpdateGroupRequest>();
        CreateMap<GroupRepresentation, UpdateGroupCommandDto>();

        // UpdateGroupManagementPermissions
        CreateMap<UpdateGroupManagementPermissionsCommand, UpdateGroupManagementPermissionsRequest>();
        CreateMap<GroupManagementPermissions, UpdateGroupManagementPermissionsCommandDto>();
    }
}
