app.config(['$stateProvider', '$urlRouterProvider', "$locationProvider", "cfpLoadingBarProvider", "USER_ROLES", "AUTH_EVENTS",
    function ($stateProvider, $urlRouterProvider, $locationProvider, cfpLoadingBarProvider, USER_ROLES, AUTH_EVENTS) {
        //$locationProvider.html5Mode({
        //    enabled: true
        //});
        $urlRouterProvider.otherwise('/');
        cfpLoadingBarProvider.includeSpinner = false;


        $stateProvider
            .state('login', {
                url: '/login',
                params: { logout: null },
                views: {
                    'body': {
                        templateUrl: 'Public/views/login/login.html',
                        controller: 'AuthCtrl'
                    }
                }
            })
        .state('register', {
            url: '/register',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/login/register.html',
                    controller: 'RegisterCtrl'
                }
            }

        })
         .state('forgotPassword', {
             url: '/ForgotPassword',
             views: {

                 'body': {
                     templateUrl: 'Public/views/login/ForgotPassword.html',
                     controller: 'ForgotPasswordCtrl'
                 }
             }

         })
         .state('home', {
             url: '/home',
             views: {
                 'header': {
                     templateUrl: 'Public/views/login/header.html'
                 },

                 'body': {
                     templateUrl: 'Public/views/home.html'

                 }
             },
             data: {
                 authorizedRoles: [USER_ROLES.SuperAdmin, USER_ROLES.GIMUser,
                     USER_ROLES.GIMAdmin, USER_ROLES.GMSUser,
                     USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
                     USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
             }

         })
        .state('selectClient', {
            url: '/selectClient',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/selectClient.html',
                    controller: 'SelectClientCtrl'
                }
            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin, USER_ROLES.GIMUser,
                    USER_ROLES.GIMAdmin, USER_ROLES.GMSUser,
                    USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
                    USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
            }
        })
        .state('CUSIP', {
            url: '/CUSIP',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/reports/cusip.html',
                    controller: 'CUSIPReportCtrl'

                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin,
              USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
              USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
            }
        })
           .state('completeRegistration', {
               url: '/CompleteRegistration?userid',
               views: {


                   'body': {
                       templateUrl: 'Public/views/login/completeRegistration.html',
                       controller: 'RegisterCtrl'
                   }
               }
           })
        .state('FullfillmentReport', {
            url: '/FullfillmentReport',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/reports/fullfillmentInfo.html',
                    controller: 'FullfillmentInfoReportCtrl'
                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin, USER_ROLES.GIMUser,
                   USER_ROLES.GIMAdmin, USER_ROLES.GMSUser,
                     USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
                   USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
            }
        })
        .state('SLINK', {
            url: '/SLINK',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/reports/slink.html',
                    controller: 'SlinkReportCtrl'
                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin, USER_ROLES.GIMUser,
                           USER_ROLES.GIMAdmin, USER_ROLES.GMSUser,
                    USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
                    USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
            }
        })
         .state('WATCHLIST', {
             url: '/WATCHLIST',
             views: {
                 'header': {
                     templateUrl: 'Public/views/login/header.html'
                 },
                 'body': {
                     templateUrl: 'Public/views/reports/watchlist.html',
                     controller: 'WatchlistReportCtrl'

                 }

             },
             data: {
                 authorizedRoles: [USER_ROLES.SuperAdmin, USER_ROLES.GIMUser,
                 USER_ROLES.GIMAdmin, USER_ROLES.GMSUser,
                 USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
                 USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
             }
         })
        .state('error', {
            url: '/error',
            params: { errorCode: "", errorDescription: "" },
            views: {
                'header': {
                    templateUrl: function (element, attrs) {
                        if (!(AUTH_EVENTS.sessionTimeout == element.errorCode || AUTH_EVENTS.notAuthenticated == element.errorCode))
                            return 'Public/views/login/header.html';
                    }
                },
                'body': {
                    templateUrl: 'Public/views/Error/Error.html',
                    controller: 'ErrorCtrl'
                }
            }
        })
          .state('CUSTOMERDOCUPDATE', {
              url: '/CUSTOMERDOCUPDATE',
              views: {
                  'header': {
                      templateUrl: 'Public/views/login/header.html'
                  },
                  'body': {
                      templateUrl: 'Public/views/reports/customerDocUpdate.html',
                      controller: 'CustomerDocUpdateReportCtrl'

                  }

              },
              data: {
                  authorizedRoles: [USER_ROLES.SuperAdmin, USER_ROLES.GMSUser,
                      USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
                      USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
              }
          })
         .state('GATEWAYDOCUPDATE', {
             url: '/GATEWAYDOCUPDATE',
             views: {
                 'header': {
                     templateUrl: 'Public/views/login/header.html'
                 },
                 'body': {
                     templateUrl: 'Public/views/reports/gatewayDocUpdate.html',
                     controller: 'GatewayDocUpdateReportCtrl'

                 }

             },
             data: {
                 authorizedRoles: [USER_ROLES.SuperAdmin, USER_ROLES.GIMUser,
              USER_ROLES.GIMAdmin, USER_ROLES.GMSUser,
              USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
              USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
             }

         })
        .state('CustomerDocUpdateDetail', {
            url: '/CustomerDocUpdateDetail',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/reports/customerDocUpdateDetail.html',
                    controller: 'CustomerDocUpdateDetailReportCtrl'

                }


            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin,
                                USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
                                USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
            }
        })
        .state('LiveUpdate', {
            url: '/LiveUpdate',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/reports/liveUpdate.html',
                    controller: 'LiveUpdateReportCtrl'
                }

            },

            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin, USER_ROLES.GIMUser,
                USER_ROLES.GIMAdmin, USER_ROLES.GMSUser,
                USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
                USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
            }
        })
        .state('profile', {
            url: '/profile',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/login/profile.html',
                    controller: 'ProfileCtrl'
                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin, USER_ROLES.GIMUser,
                    USER_ROLES.GIMAdmin, USER_ROLES.GMSUser,
                    USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin, USER_ROLES.AllianceBernsteinUser,
                    USER_ROLES.TransamericaAdmin, USER_ROLES.TransamericaUser]
            }

        })
         .state('Users', {
             url: '/Users',
             views: {
                 'header': {
                     templateUrl: 'Public/views/login/header.html'
                 },
                 'body': {
                     templateUrl: 'Public/views/login/users.html',
                     controller: 'UsersCtrl'
                 }

             },
             data: {
                 authorizedRoles: [USER_ROLES.SuperAdmin,
                    USER_ROLES.GIMAdmin,
                    USER_ROLES.GMSAdmin, USER_ROLES.AllianceBernsteinAdmin,
                    USER_ROLES.TransamericaAdmin]
             }

         })
        .state('TRPDocumentStatus', {
            url: '/documentStatus',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/TRP/documentStatus.html',
                    controller: 'TRPDocumentStatusCtrl'

                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin,
                   USER_ROLES.TRPUser, USER_ROLES.TRPAdmin
                ]
            }
        })
        .state('TRPCustomerFLT', {
            url: '/customerFLT',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/TRP/customerFLT.html',
                    controller: 'TRPCustomerFLTCtrl'

                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin,
                   USER_ROLES.TRPUser, USER_ROLES.TRPAdmin
                ]
            }
        })
        .state('TRPCustomerDocs', {
            url: '/customerDocs',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/TRP/customerDocs.html',
                    controller: 'TRPCustomerDocsCtrl'

                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin,
                   USER_ROLES.TRPUser, USER_ROLES.TRPAdmin
                ]
            }
        })
        .state('TRPBlankFLTCUSIP', {
            url: '/blankFLTCUSIP',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/TRP/blankFLTCUSIP.html',
                    controller: 'TRPBlankFLTCUSIPCtrl'
                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin,
                   USER_ROLES.TRPUser, USER_ROLES.TRPAdmin
                ]
            }
        })
        .state('TRPMissingDocs', {
            url: '/missingDocInfo',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/TRP/missingDocument.html',
                    controller: 'TRPMissingDocumentCtrl'

                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin,
                   USER_ROLES.TRPUser, USER_ROLES.TRPAdmin
                ]
            }
        })
        .state('TRPMissingRPCusip', {
            url: '/missingRPCusip',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/TRP/missingRPCusip.html',
                    controller: 'TRPMissingRPCusipCtrl'

                }

            },
            data: {
                authorizedRoles: [USER_ROLES.SuperAdmin,
                   USER_ROLES.TRPUser, USER_ROLES.TRPAdmin
                ]
            }
        })
        .state('DailyUpdateReport', {
            url: '/dailyUpdateReport',
            views: {
                'header': {
                    templateUrl: 'Public/views/login/header.html'
                },
                'body': {
                    templateUrl: 'Public/views/reports/DailyUpdate.html',
                    controller: 'DailyUpdateReportCtrl'

                }

            }
           
        })

    }]);
